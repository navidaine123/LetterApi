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

        [HttpGet]
        public async Task<IActionResult> ShowInbox()
        {
            if (User.Claims == null)
                return BadRequest(ResponseMessage.NotAuthentication);

            var id = _userService.GetUSerIDFromUserClaims(User.Claims);
            var inboxMessages = await _messageServices.GetMessagesRecievedbyAsync(id);
            return Ok(inboxMessages);
        }

        [HttpGet]
        public async Task<IActionResult> ShowOutBox()
        {
            if (User.Claims == null)
                return BadRequest(ResponseMessage.NotAuthentication);

            var id = _userService.GetUSerIDFromUserClaims(User.Claims);

            var a = await _messageServices.GetSendOrDraftMessagesByAync(id, true);
            return Ok(a);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ShowDraftBox()
        {
            if (User.Claims == null)
                return BadRequest(ResponseMessage.NotAuthentication);

            var id = _userService.GetUSerIDFromUserClaims(User.Claims);

            var a = await _messageServices.GetSendOrDraftMessagesByAync(id, false);
            return Ok(a);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ShowImportantMessaes()
        {
            if (User.Claims == null)
                return BadRequest(ResponseMessage.NotAuthentication);

            var id = _userService.GetUSerIDFromUserClaims(User.Claims);

            var a = await _messageServices.GetSendOrDraftMessagesByAync(id, false);
            return Ok(a);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteFromOutboxOrDraft(MsgBoxDTO messageDto)
            => Ok(await _messageServices.DeleteSentOrDraftMessage(messageDto));

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteFromInbox(MsgBoxDTO messageDto)
            => Ok(await _messageServices.DeleteRecievedMessage(messageDto));

        /// <summary>
        /// summary
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ShowDeletedMessage()
        {
            var deletedMessages =
                await _messageServices.GetDeletedMessage(_userService.GetUSerIDFromUserClaims(User.Claims));
            if (deletedMessages != null)
                return Ok(deletedMessages);

            return Ok("no message has deleted");
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