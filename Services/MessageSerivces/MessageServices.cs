using AutoMapper;
using Models.Enums;
using Models.MessageModels;
using Repository;
using Services.Dto.MessageDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MessageSerivces
{
    public interface IMessageServices
    {
        Task<Message> CreateMessageAsync(Guid creator);

        Task<bool> MessageAction(SendMsgDTO messageDto, bool isSent, Guid senderId);

        Task<List<MsgBoxDTO>> GetSendOrDraftMessagesByAync(Guid id, bool isSent);

        Task<List<MsgBoxDTO>> GetMessagesRecievedbyAsync(Guid id);

        Task<string> DeleteSentOrDraftMessage(MsgBoxDTO messageDto);

        Task<string> DeleteRecievedMessage(MsgBoxDTO messageDto);

        Task<List<MsgBoxDTO>> GetDeletedMessage(Guid id);

        Task<bool> ForwardMessageAsync(MsgBoxDTO DTO);
    }

    public class MessageServices : IMessageServices
    {
        #region privatefields

        private readonly IMessageRepository _messageRepository;
        private readonly IMessageSenderRepository _messageSenderRepository;
        private readonly MessageRecieverRepository _messageRecieverRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        #endregion privatefields

        #region constructors

        public MessageServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _messageRepository = _unitOfWork.MessageRepository;
            _messageSenderRepository = _unitOfWork.MessageSenderRepository;
            _mapper = mapper;
            _messageRecieverRepository = _unitOfWork.MessageRecieverRepository;
        }

        #endregion constructors

        public async Task<Message> CreateMessageAsync(Guid creatorId)
        {
            var message = new Message()
            {
                CreatedById = creatorId,
                MessageCode = await GenerateMessageCodeAsync(),
            };
            message.MessageNumber = GenerateMessageNumber(message.MessageCode);

            await _unitOfWork.MessageRepository.AddAsync(message);
            await _unitOfWork.SaveAsync();

            return message;
        }

        /// <summary>
        /// if isSent is true send the message and if be false draft it
        /// </summary>
        /// <param name="messageDto"></param>
        /// <param name="isSent"></param>
        /// <returns></returns>
        public async Task<bool> MessageAction(SendMsgDTO messageDto, bool isSent, Guid senderId)
        {
            try
            {
                if (messageDto.To == null)
                    return false;
                var message = await _unitOfWork.MessageRepository.GetAsync(messageDto.Id);
                if (message == null)
                    return false;
                _mapper.Map(messageDto, message);

                var messageSender = new MessageSender
                {
                    MessageId = messageDto.Id,
                    IsSent = isSent,
                    UserId = senderId
                };

                var messageRecievers = await MessageRecieversListAsync(messageDto, messageSender);

                var messageResult = await _messageRepository.UpdateAsync(message, message.Id);
                var messageSenderResult = await _messageSenderRepository.AddAsync(messageSender);
                var messageRecieverResult = await _messageRecieverRepository.AddRangeAsync(messageRecievers);

                await _unitOfWork.SaveAsync();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private async Task<string> GenerateMessageCodeAsync()
        {
            var message = await _messageRepository.GetLastOfMessagesAsync();

            string newMessageCode =
                message == null ? "1" : (Convert.ToInt32(message.MessageCode)) + 1.ToString();

            return newMessageCode;
        }

        private string GenerateMessageNumber(string code) => code + DateTime.UtcNow.Date.ToString();

        private async Task<List<MessageReciever>> MessageRecieversListAsync(SendMsgDTO messageDto, MessageSender messageSender)
        {
            var messageReciever = new List<MessageReciever>();

            if (messageDto.Cc != null)
            {
                foreach (var item in messageDto.Cc)
                {
                    messageReciever.Add(new MessageReciever()
                    {
                        MessageId = messageDto.Id,
                        IsCc = true,
                        MessageSender = messageSender,
                        UserId = item,
                    });
                }
            }
            foreach (var item in messageDto.To)
            {
                messageReciever.Add(new MessageReciever()
                {
                    MessageId = messageDto.Id,
                    IsCc = false,
                    MessageSender = messageSender,
                    UserId = item
                });
            }

            return messageReciever;
        }

        public async Task<List<MsgBoxDTO>> GetMessagesRecievedbyAsync(Guid id)
        {
            var messages =
                (await _messageRecieverRepository.GetMessagesRecieveByAync(id))
                .Where(x => x.DeletedDate == null);

            var messageDto = _mapper.Map<List<MsgBoxDTO>>(messages);

            return null;
        }

        /// <summary>
        /// if is sent is true returned sent messages else returned draft it
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isSent"></param>
        /// <returns></returns>
        public async Task<List<MsgBoxDTO>> GetSendOrDraftMessagesByAync(Guid id, bool isSent)
        {
            var messages =
                (await _messageSenderRepository.GetMessagesSendByAync(id))
                .Where(x => x.DeletedDate == null && x.IsSent == isSent).ToList();

            List<MsgBoxDTO> msgBoxDTO = new List<MsgBoxDTO>();

            _mapper.Map(messages, msgBoxDTO);

            return msgBoxDTO;
        }

        public async Task<string> DeleteSentOrDraftMessage(MsgBoxDTO messageDto)
        {
            var message = _mapper.Map<MessageSender>(messageDto);
            message.DeletedDate = DateTime.UtcNow;

            return await _messageSenderRepository.DeleteFromSenders(message);
        }

        public async Task<string> DeleteRecievedMessage(MsgBoxDTO messageDto)
        {
            var message = _mapper.Map<MessageReciever>(messageDto);
            message.DeletedDate = DateTime.UtcNow;

            return await _messageRecieverRepository.DeleteFromReciever(message);
        }

        public async Task<List<MsgBoxDTO>> GetDeletedMessage(Guid id)
        {
            var inboxMessages = _mapper.Map<List<MsgBoxDTO>>
                (
                (await _messageRecieverRepository.GetMessagesRecieveByAync(id))
                .Where(x => x.DeletedDate != null)
                );

            var outboxMessages = _mapper.Map<List<MsgBoxDTO>>
                (
                (await _messageSenderRepository.GetMessagesSendByAync(id))
                .Where(x => x.DeletedDate != null)
                );

            var res = new List<MsgBoxDTO>();
            res.AddRange(inboxMessages);
            res.AddRange(outboxMessages);

            return res;
        }

        public async Task<bool> ForwardMessageAsync(MsgBoxDTO DTO)
        {
            var messageSender = _mapper.Map<MessageSender>(DTO);
            messageSender.Id = Guid.NewGuid();
            messageSender.ResendOnId = DTO.Id;

            foreach (var item in DTO.ResendToIdList)
            {
                var messageReciever = new MessageReciever
                {
                    Id = Guid.NewGuid(),
                    UserId = item,
                    MessageId = messageSender.MessageId,
                    MessageSenderId = messageSender.Id,
                    IsCc = false,
                };
                messageSender.MessageRecievers.Add(messageReciever);
            }

            var addMessageSender = await _messageSenderRepository.AddAsync(messageSender);
            //var addMessageReciever = await _messageRecieverRepository.AddRangeAsync(messageSender.MessageRecievers);

            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<List<MsgBoxDTO>> GetIamportantSentMessages(Guid id)
            => (await GetSendOrDraftMessagesByAync(id, true))
            .Where(x => x.ImportanceLevel == ImportanceLevel.Important)
            .ToList();
    }
}