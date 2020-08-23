using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.MessageModels;
using Newtonsoft.Json;
using Services.Dto.MessageDto;
using Services.MessageSerivces;
using Services.Shared;
using Services.UserServices;
using Test.Models.UserModels;

namespace Test.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageServices _messageServices;
        private readonly IUserService _userService;

        #region Contructors

        public MessagesController(IMessageServices messageServices, IUserService userService)
        {
            _messageServices = messageServices;
            _userService = userService;
        }

        #endregion Contructors

        /// <summary>
        /// Create a new message and generate message number
        /// </summary>
        /// <returns>a new message</returns>
        [HttpGet("createMessage")]
        [Authorize]
        public async Task<IActionResult> CreateAsync()
        {
            var creatorId = _userService.GetUSerIDFromUserClaims(User.Claims);

            var messageDto = await _messageServices.CreateMessageAsync(creatorId);

            return Ok(messageDto);
        }

        /// <summary>
        /// send a message that was created
        /// </summary>
        /// <param name="messageDto">created message with content and subject</param>
        /// <returns>a comment about message sending status</returns>
        [HttpPost("sendMessage")]
        [Authorize]
        public async Task<IActionResult> SendMessageAsync(SendMsgDTO messageDto)
        {
            var senderId = _userService.GetUSerIDFromUserClaims(User.Claims);

            if (await _messageServices.MessageAction(messageDto, true, senderId))
                return Ok(ResponseMessage.Ok);
            return BadRequest(ResponseMessage.BadRequest);
        }

        /// <summary>
        /// draft a message that was created
        /// </summary>
        /// <param name="messageDto">created message with content and subject</param>
        /// <returns>a comment about message drafting status</returns>
        [HttpPost("draftMessage")]
        [Authorize]
        public async Task<IActionResult> DraftMessageAsync(SendMsgDTO messageDto)
        {
            var senderId = _userService.GetUSerIDFromUserClaims(User.Claims);

            if (await _messageServices.MessageAction(messageDto, false, senderId))
                return Ok(ResponseMessage.Ok);
            return BadRequest(ResponseMessage.BadRequest);
        }

        /// <summary>
        /// show messages that user recieved
        /// </summary>
        /// <returns>recieved messages</returns>
        [HttpGet("inBox")]
        [Authorize]
        public async Task<IActionResult> ShowInboxAsync()
        {
            var id = _userService.GetUSerIDFromUserClaims(User.Claims);
            var inboxMessages = await _messageServices.GetMessagesRecievedbyAsync(id);

            return Ok(inboxMessages);
        }

        /// <summary>
        /// show messages that user sent
        /// </summary>
        /// <returns>sent messages</returns>
        [HttpGet("outBox")]
        [Authorize]
        public async Task<IActionResult> ShowOutBoxAsync()
        {
            var id = _userService.GetUSerIDFromUserClaims(User.Claims);

            var outBox = await _messageServices.GetSendOrDraftMessagesByIdAync(id, true);
            return Ok(outBox);
        }

        /// <summary>
        /// show messages that user draft
        /// </summary>
        /// <returns>drafted messages</returns>
        [HttpGet("draftBox")]
        [Authorize]
        public async Task<IActionResult> ShowDraftBox()
        {
            var id = _userService.GetUSerIDFromUserClaims(User.Claims);

            var draftBox = await _messageServices.GetSendOrDraftMessagesByIdAync(id, false);
            return Ok(draftBox);
        }

        /// <summary>
        /// show messages that user recieved or sent that they are important
        /// </summary>
        /// <returns>important messages</returns>
        [HttpGet("importantMessages")]
        [Authorize]
        public async Task<IActionResult> ShowImportantMessaesAsync()
        {
            var id = _userService.GetUSerIDFromUserClaims(User.Claims);

            var impMsg = await _messageServices.GetImportantSentMessages(id);
            return Ok(impMsg);
        }

        /// <summary>
        /// set message ismarked value true
        /// </summary>
        /// <returns>true or false</returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> SetMarkRcievedMessagesAsync(Guid id)
            => Ok(await _messageServices.SetMarkRecievedMessageAsync(id));

        /// <summary>
        /// if sent message not marked set message ismarked value true
        /// else unmarked message and set ismarked value false
        /// </summary>
        /// <returns>true or false</returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> SetMarkSentMessagesAsync(Guid id)
            => Ok(await _messageServices.SetMarkRecievedMessageAsync(id));

        /// <summary>
        /// if recieved message not marked set message ismarked value true
        /// else unmarked message and set ismarked value false
        /// </summary>
        /// <returns>true or false</returns>
        [HttpGet("markedMessage")]
        [Authorize]
        public async Task<IActionResult> ShowMarkedMessagesAsync()
        {
            if (User.Claims == null)
                return BadRequest(ResponseMessage.NotAuthentication);

            var id = _userService.GetUSerIDFromUserClaims(User.Claims);

            var result = await _messageServices.GetMarkedMessage(id);

            return Ok(result);
        }

        /// <summary>
        /// delete messages that user send or draft
        /// </summary>
        /// <returns>delete sent or draft messages</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteFromOutboxOrDraftAsync(Guid id)
            => Ok(await _messageServices.DeleteSentOrDraftMessage(id));

        /// <summary>
        /// delete messages that user recieved
        /// </summary>
        /// <returns>delete recieved messages</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteFromInboxAsync(Guid id)
            => Ok(await _messageServices.DeleteRecievedMessage(id));

        /// <summary>
        /// restore deleted message
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> RestoreDeletedMessageAsync(Guid id)
            => Ok(await _messageServices.RestoreDeletedMessageAsync(id));

        /// <summary>
        /// show messages that user deletd
        /// </summary>
        /// <returns>deleted messages</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ShowDeletedMessage()
        {
            var deletedMessages =
                await _messageServices
                .GetDeletedMessage(_userService.GetUSerIDFromUserClaims(User.Claims));
            if (deletedMessages != null)
                return Ok(deletedMessages);

            return Ok("no message has deleted");
        }

        /// <summary>
        /// send message from draft box set issent property true
        /// </summary>
        /// <param name="id">message sender id</param>
        /// <returns>send status</returns>
        [HttpGet("getForEdit/{id}")]
        [Authorize]
        public async Task<IActionResult> GetMessageForEditAsync(Guid id)
        {
            return Ok(await _messageServices.GetMessageForEditAsync(id));
        }

        /// <summary>
        /// give forwMsgDto to it and forward message
        /// </summary>
        /// <param name="forwardMsgDto">prove is external content toid is contact that you want forward to them </param>
        /// <returns>message status</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ForwardMessageAsync(ForwardMsgDto forwardMsgDto)
        {
            var id = _userService.GetUSerIDFromUserClaims(User.Claims);

            if (await _messageServices.ForwardMessageAsync(forwardMsgDto, id))
                return Ok("پیام ارجاع داده شد");
            return BadRequest("خطایی رخ داده است");
        }

        [HttpGet("getForReply/{id}")]
        [Authorize]
        public async Task<IActionResult> GetReplyMessageAsync(Guid replyToMessageId)
        {
            var senderId = _userService.GetUSerIDFromUserClaims(User.Claims);

            return Ok(await _messageServices.GetReplyMessageAsync(senderId, replyToMessageId));
        }

        [HttpGet("getForRead")]
        [Authorize]
        public async Task<IActionResult> GetMessageForRead(Guid messageId, Guid recieverId)
        {
            var userId = _userService.GetUSerIDFromUserClaims(User.Claims);

            return Ok(await _messageServices.GetMessageForRead(messageId, userId, recieverId));
        }
    }
}