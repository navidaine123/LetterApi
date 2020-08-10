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
        MessageDto CreateMessage(Guid creator);

        Task<bool> MessageAction(MessageDto messageDto, bool isSent, Guid senderId);

        Task<List<MessageDto>> GetSendOrDraftMessagesByAync(Guid id, bool isSent);

        Task<List<MessageDto>> GetMessagesRecievedbyAsync(Guid id);

        Task<string> DeleteSentOrDraftMessage(MessageDto messageDto);

        Task<string> DeleteRecievedMessage(MessageDto messageDto);

        Task<List<MessageDto>> GetDeletedMessage(Guid id);

        Task<bool> ForwardMessageAsync(MessageDto messageDto);
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

        public MessageDto CreateMessage(Guid creator)
        {
            var messageDto = new MessageDto()
            {
                CreatedById = creator,
                Id = Guid.NewGuid(),
                MessageCode = GenerateMessageCode().GetAwaiter().GetResult(),
            };
            messageDto.MessageNumber = GenerateMessageNumber(messageDto.MessageCode);

            return messageDto;
        }

        /// <summary>
        /// if isSent is true send the message and if be false draft it
        /// </summary>
        /// <param name="messageDto"></param>
        /// <param name="isSent"></param>
        /// <returns></returns>
        public async Task<bool> MessageAction(MessageDto messageDto, bool isSent, Guid senderId)
        {
            try
            {
                if (messageDto.To == null)
                    return false;

                var message = _mapper.Map<Message>(messageDto);
                var messageSender = new MessageSender
                {
                    Message = message,
                    IsSent = isSent,
                    Id = Guid.NewGuid(),
                    UserId = senderId,
                };

                var messageRecievers = MessageRecieversList(messageDto, messageSender, message);

                var messageResult = await _messageRepository.AddAsync(message);
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

        private async Task<string> GenerateMessageCode()
        {
            var message = await _messageRepository.GetLastOfMessagesAsync();

            string newMessageCode =
                message == null ? "1" : (Convert.ToInt32(message.MessageCode)) + 1.ToString();

            return newMessageCode;
        }

        private string GenerateMessageNumber(string code) => code + DateTime.UtcNow.Date.ToString();

        public List<MessageReciever> MessageRecieversList(MessageDto messageDto, MessageSender messageSender, Message message)
        {
            var messageReciever = new List<MessageReciever>();

            if (messageDto.Cc != null)
            {
                foreach (var item in messageDto.Cc)
                {
                    messageReciever.Add(new MessageReciever()
                    {
                        Id = Guid.NewGuid(),
                        Message = message,
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
                    Id = Guid.NewGuid(),
                    Message = message,
                    IsCc = false,
                    MessageSender = messageSender,
                    UserId = item,
                });
            }

            return messageReciever;
        }

        public async Task<List<MessageDto>> GetMessagesRecievedbyAsync(Guid id)
        {
            var messages =
                (await _messageRecieverRepository.GetMessagesRecieveByAync(id))
                .Where(x => x.DeletedDate == null);

            var messageDto = _mapper.Map<List<MessageDto>>(messages);

            return null;
        }

        /// <summary>
        /// if is sent is true returned sent messages else returned draft it
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isSent"></param>
        /// <returns></returns>
        public async Task<List<MessageDto>> GetSendOrDraftMessagesByAync(Guid id, bool isSent)
        {
            var messages =
                (await _messageSenderRepository.GetMessagesSendByAync(id))
                .Where(x => x.DeletedDate == null);

            var messageDto = _mapper.Map<List<MessageDto>>(messages);

            return messageDto;
        }

        public async Task<string> DeleteSentOrDraftMessage(MessageDto messageDto)
        {
            var message = _mapper.Map<MessageSender>(messageDto);
            message.DeletedDate = DateTime.UtcNow;

            return await _messageSenderRepository.DeleteFromSenders(message);
        }

        public async Task<string> DeleteRecievedMessage(MessageDto messageDto)
        {
            var message = _mapper.Map<MessageReciever>(messageDto);
            message.DeletedDate = DateTime.UtcNow;

            return await _messageRecieverRepository.DeleteFromReciever(message);
        }

        public async Task<List<MessageDto>> GetDeletedMessage(Guid id)
        {
            var inboxMessages = _mapper.Map<List<MessageDto>>
                (
                (await _messageRecieverRepository.GetMessagesRecieveByAync(id))
                .Where(x => x.DeletedDate != null)
                );

            var outboxMessages = _mapper.Map<List<MessageDto>>
                (
                (await _messageSenderRepository.GetMessagesSendByAync(id))
                .Where(x => x.DeletedDate != null)
                );

            var res = new List<MessageDto>();
            res.AddRange(inboxMessages);
            res.AddRange(outboxMessages);

            return res;
        }

        public async Task<bool> ForwardMessageAsync(MessageDto messageDto)
        {
            var messageSender = _mapper.Map<MessageSender>(messageDto);
            messageSender.Id = Guid.NewGuid();

            var messageReciever = new MessageReciever
            {
                Id = Guid.NewGuid(),
                UserId = messageSender.ResendOnId.Value,
                MessageId = messageSender.MessageId,
                MessageSenderId = messageSender.Id,
            };

            var addMessageSender = await _messageSenderRepository.AddAsync(messageSender);
            var addMessageReciever = await _messageRecieverRepository.AddAsync(messageReciever);

            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<List<MessageDto>> GetIamportantSentMessages(Guid id)
            => (await GetSendOrDraftMessagesByAync(id, true))
            .Where(x => x.ImportanceLevel == ImportanceLevel.Important)
            .ToList();
    }
}