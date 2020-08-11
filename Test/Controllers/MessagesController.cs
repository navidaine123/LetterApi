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
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CreateAsync()
        {
            var userClaims = User.Claims;
            if (userClaims == null)
                return BadRequest(ResponseMessage.NotAuthentication);

            var creatorId = _userService.GetUSerIDFromUserClaims(userClaims);

            var message = await _messageServices.CreateMessageAsync(creatorId);

            return Ok(message);
        }

        /// <summary>
        /// send a message that was created
        /// </summary>
        /// <param name="messageDto">created message with content and subject</param>
        /// <returns>a comment about message sending status</returns>
        [HttpPost]
        public async Task<IActionResult> SendMessage(SendMsgDTO messageDto)
        {
            if (User.Claims == null)
                return BadRequest(ResponseMessage.NotAuthentication);

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
        [HttpPost]
        public async Task<IActionResult> DraftMessage(SendMsgDTO messageDto)
        {
            if (User.Claims == null)
                return BadRequest(ResponseMessage.NotAuthentication);

            var senderId = _userService.GetUSerIDFromUserClaims(User.Claims);

            if (await _messageServices.MessageAction(messageDto, false, senderId))
                return Ok(ResponseMessage.Ok);
            return BadRequest(ResponseMessage.BadRequest);
        }

        /// <summary>
        /// show messages that user recieved
        /// </summary>
        /// <returns>recieved messages</returns>
        [HttpGet]
        public async Task<IActionResult> ShowInbox()
        {
            if (User.Claims == null)
                return BadRequest(ResponseMessage.NotAuthentication);

            var id = _userService.GetUSerIDFromUserClaims(User.Claims);
            var inboxMessages = await _messageServices.GetMessagesRecievedbyAsync(id);
            return Ok(inboxMessages);
        }

        /// <summary>
        /// show messages that user sent
        /// </summary>
        /// <returns>sent messages</returns>
        [HttpGet]
        public async Task<IActionResult> ShowOutBox()
        {
            if (User.Claims == null)
                return BadRequest(ResponseMessage.NotAuthentication);

            var id = _userService.GetUSerIDFromUserClaims(User.Claims);

            var a = await _messageServices.GetSendOrDraftMessagesByIdAync(id, true);
            return Ok(a);
        }

        /// <summary>
        /// show messages that user draft
        /// </summary>
        /// <returns>drafted messages</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ShowDraftBox()
        {
            if (User.Claims == null)
                return BadRequest(ResponseMessage.NotAuthentication);

            var id = _userService.GetUSerIDFromUserClaims(User.Claims);

            var a = await _messageServices.GetSendOrDraftMessagesByIdAync(id, false);
            return Ok(a);
        }

        /// <summary>
        /// show messages that user recieved or sent that they are important
        /// </summary>
        /// <returns>important messages</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ShowImportantMessaesAsync()
        {
            if (User.Claims == null)
                return BadRequest(ResponseMessage.NotAuthentication);

            var id = _userService.GetUSerIDFromUserClaims(User.Claims);

            var data = await _messageServices.GetImportantSentMessages(id);
            return Ok(data);
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
        [HttpGet]
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
        public async Task<IActionResult> DeleteFromOutboxOrDraft(Guid id)
            => Ok(await _messageServices.DeleteSentOrDraftMessage(id));

        /// <summary>
        /// delete messages that user recieved
        /// </summary>
        /// <returns>delete recieved messages</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteFromInbox(Guid id)
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
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> SendMessageFromDraftBoxAsync(Guid id)
        {
            return Ok(await _messageServices.SendFromDraftAsync(id));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ForwardMessage(MsgBoxDTO dTO)
        {
            if (await _messageServices.ForwardMessageAsync(dTO))
                return Ok("پیام ارجاع داده شد");
            return BadRequest("خطایی رخ داده است");
        }
    }
}