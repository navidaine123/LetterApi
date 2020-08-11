﻿using AutoMapper;
using Models.Enums;
using Models.MessageModels;
using Repository;
using Services.Dto.MessageDto;
using Services.Shared;
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

        Task<List<MsgBoxDTO>> GetSendOrDraftMessagesByIdAync(Guid id, bool isSent);

        Task<List<MsgBoxDTO>> GetMessagesRecievedbyAsync(Guid id);

        Task<string> DeleteSentOrDraftMessage(Guid id);

        Task<string> DeleteRecievedMessage(Guid id);

        Task<List<MsgBoxDTO>> GetDeletedMessage(Guid id);

        Task<bool> ForwardMessageAsync(ForwardMsgDto forwardMsgDto, Guid userID);

        Task<List<MsgBoxDTO>> GetImportantSentMessages(Guid id);

        Task<List<MsgBoxDTO>> GetMarkedMessage(Guid id);

        Task<bool> SetMarkRecievedMessageAsync(Guid id);

        Task<bool> SetMarkSentMessageAsync(Guid id);

        Task<string> RestoreDeletedMessageAsync(Guid id);

        Task<string> SendFromDraftAsync(Guid id);
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
                .Where(x => x.DeletedDate == null)
                .ToList();

            List<MsgBoxDTO> msgBoxDTO = new List<MsgBoxDTO>();

            _mapper.Map(messages, msgBoxDTO);

            return msgBoxDTO;
        }

        /// <summary>
        /// if is sent is true returned sent messages else returned draft it
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isSent"></param>
        /// <returns></returns>
        public async Task<List<MsgBoxDTO>> GetSendOrDraftMessagesByIdAync(Guid id, bool isSent)
        {
            var messages =
                (await _messageSenderRepository.GetMessagesSendByAync(id))
                .Where(x => x.DeletedDate == null && x.IsSent == isSent)
                .ToList();

            List<MsgBoxDTO> msgBoxDTO = new List<MsgBoxDTO>();

            _mapper.Map(messages, msgBoxDTO);

            return msgBoxDTO;
        }

        public async Task<string> DeleteSentOrDraftMessage(Guid id)
        {
            MessageSender message = await _messageSenderRepository.GetAsync(id);

            message.DeletedDate = DateTime.UtcNow;

            message = await _messageSenderRepository
                .UpdateAsync(message, message.Id);

            await _unitOfWork.SaveAsync();

            return "حذف شد";
        }

        public async Task<string> DeleteRecievedMessage(Guid id)
        {
            MessageReciever msgReciever = await _messageRecieverRepository.GetAsync(id);

            msgReciever.DeletedDate = DateTime.UtcNow;

            msgReciever =
                await _messageRecieverRepository
                .UpdateAsync(msgReciever, msgReciever.Id);

            await _unitOfWork.SaveAsync();

            return "حذف شد";
        }

        public async Task<List<MsgBoxDTO>> GetDeletedMessage(Guid id)
        {
            var inboxMessages = _mapper.Map<List<MsgBoxDTO>>
                ((await _messageRecieverRepository
                .GetMessagesRecieveByAync(id))
                .Where(x => x.DeletedDate != null));

            var outboxMessages = _mapper.Map<List<MsgBoxDTO>>
                ((await _messageSenderRepository
                .GetMessagesSendByAync(id))
                .Where(x => x.DeletedDate != null));

            return inboxMessages
                .Concat(outboxMessages)
                .ToList();
        }

        public async Task<List<MsgBoxDTO>> GetMarkedMessage(Guid id)
        {
            var inboxMessages = _mapper.Map<List<MsgBoxDTO>>
                ((await _messageRecieverRepository
                .GetMessagesRecieveByAync(id))
                .Where(x => x.IsMarked));

            var outboxMessages = _mapper.Map<List<MsgBoxDTO>>
                ((await _messageSenderRepository
                .GetMessagesSendByAync(id))
                .Where(x => x.IsMarked));

            return inboxMessages
                .Concat(outboxMessages)
                .ToList();
        }

        public async Task<bool> ForwardMessageAsync(ForwardMsgDto forwardMsgDto, Guid userID)
        {
            var reSendOn =
                await _messageRecieverRepository
                .GetAsync(forwardMsgDto.MessageRecieverId);

            var messageRecievers = new List<MessageReciever>();

            var messageSender = new MessageSender
            {
                IsSent = true,
                ResendOnId = forwardMsgDto.MessageRecieverId,
                MessageId = reSendOn.MessageId,
                Id = Guid.NewGuid(),
                UserId = userID,
                Prove = forwardMsgDto.Prove,
            };

            foreach (var item in forwardMsgDto.ToIds)
            {
                messageRecievers.Add(new MessageReciever
                {
                    Id = Guid.NewGuid(),
                    IsCc = false,
                    MessageId = reSendOn.MessageId,
                    MessageSenderId = messageSender.Id,
                    UserId = item,
                });
            }

            var messageSenderResult =
                await _messageSenderRepository
                .AddAsync(messageSender);

            var messageRcieversResult =
                await _messageRecieverRepository
                .AddRangeAsync(messageRecievers);

            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<List<MsgBoxDTO>> GetImportantSentMessages(Guid id)
        => (await GetMessagesRecievedbyAsync(id))
            .Concat((await GetSendOrDraftMessagesByIdAync(id, true)))
            .ToList()
            .Where(p => p.ImportanceLevel == ImportanceLevel.Important)
            .ToList();

        public async Task<bool> SetMarkRecievedMessageAsync(Guid id)
        {
            var message =
                await _messageRecieverRepository
                .GetAsync(id);

            if (message.IsMarked)
                message.IsMarked = false;
            else
                message.IsMarked = true;

            message =
                await _messageRecieverRepository
                .UpdateAsync(message, id);

            await _unitOfWork.SaveAsync();

            return message.IsMarked;
        }

        public async Task<bool> SetMarkSentMessageAsync(Guid id)
        {
            var message =
                await _messageSenderRepository
                .GetAsync(id);

            if (message.IsMarked)
                message.IsMarked = false;
            else
                message.IsMarked = true;

            message =
                await _messageSenderRepository
                .UpdateAsync(message, id);

            await _unitOfWork.SaveAsync();

            return message.IsMarked;
        }

        public async Task<string> RestoreDeletedMessageAsync(Guid id)
        {
            if (await _messageRecieverRepository.GetAsync(id) != null)
            {
                var message =
                    await _messageRecieverRepository
                    .GetAsync(id);

                message.DeletedDate = null;

                message =
                    await _messageRecieverRepository
                    .UpdateAsync(message, id);

                await _unitOfWork.SaveAsync();

                return "پیام بازگردانده شد";
            }
            else
            {
                var message =
                    await _messageSenderRepository
                    .GetAsync(id);

                message.DeletedDate = null;

                message =
                    await _messageSenderRepository
                    .UpdateAsync(message, id);

                await _unitOfWork.SaveAsync();

                return "پیام بازگردانده شد";
            }
        }

        public async Task<string> SendFromDraftAsync(Guid id)
        {
            var message =
                await _messageSenderRepository
                .GetAsync(id);

            message.IsSent = true;

            message =
                await _messageSenderRepository
                .UpdateAsync(message, id);

            await _unitOfWork.SaveAsync();

            return "پیام ارسال شد";
        }

        
    }
}