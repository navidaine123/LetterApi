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

namespace Test.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageServices _messageServices;

        #region Contructors

        public MessagesController(IMessageServices messageServices)
        {
            _messageServices = messageServices;
        }

        #endregion Contructors

        [HttpGet]
        public IActionResult Create()
        {
            if (User.Claims == null)
                return BadRequest(ResponseMessage.NotAuthentication);

            var creatorId = GetUSerID(User.Claims);

            var message = _messageServices.CreateMessage(creatorId);
            string jasonMessage = JsonConvert.SerializeObject(message);

            return Ok(jasonMessage);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(MessageDto messageDto)
        {
            if (User.Claims == null)
                return BadRequest(ResponseMessage.NotAuthentication);

            var senderId = GetUSerID(User.Claims);

            if (await _messageServices.MessageAction(messageDto, true, senderId))
                return Ok(ResponseMessage.Ok);
            return BadRequest(ResponseMessage.BadRequest);
        }

        [HttpPost]
        public async Task<IActionResult> DraftMessage(MessageDto messageDto)
        {
            if (User.Claims == null)
                return BadRequest(ResponseMessage.NotAuthentication);

            var senderId = GetUSerID(User.Claims);

            if (await _messageServices.MessageAction(messageDto, true, senderId))
                return Ok(ResponseMessage.Ok);
            return BadRequest(ResponseMessage.BadRequest);
        }

        [HttpGet]
        public async Task<IActionResult> ShowInbox()
        {
            if (User.Claims == null)
                return BadRequest(ResponseMessage.NotAuthentication);

            var id = GetUSerID(User.Claims);
            var a = await _messageServices.GetMessagesRecievedbyAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> ShowOutBox()
        {
            if (User.Claims == null)
                return BadRequest(ResponseMessage.NotAuthentication);

            var id = GetUSerID(User.Claims);

            var a = await _messageServices.GetSendOrDraftMessagesByAync(id, true);
            return Ok(a);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ShowDraftBox()
        {
            if (User.Claims == null)
                return BadRequest(ResponseMessage.NotAuthentication);

            var id = GetUSerID(User.Claims);

            var a = await _messageServices.GetSendOrDraftMessagesByAync(id, false);
            return Ok(a);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ShowImportantMessaes()
        {
            if (User.Claims == null)
                return BadRequest(ResponseMessage.NotAuthentication);

            var id = GetUSerID(User.Claims);

            var a = await _messageServices.GetSendOrDraftMessagesByAync(id, false);
            return Ok(a);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteFromOutboxOrDraft(MessageDto messageDto)
            => Ok(await _messageServices.DeleteSentOrDraftMessage(messageDto));

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteFromInbox(MessageDto messageDto)
            => Ok(await _messageServices.DeleteRecievedMessage(messageDto));

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ShowDeletedMessage()
        {
            var deletedMessages =
                await _messageServices.GetDeletedMessage(GetUSerID(User.Claims));
            if (deletedMessages != null)
                return Ok(deletedMessages);

            return Ok("no message has deleted");
        }

        //[HttpPost]
        //[Authorize]
        //public async Task<IActionResult> ForwardMessage(MessageDto messageDto)
        //{
        //}

        public Guid GetUSerID(IEnumerable<Claim> claims) =>
            Guid.Parse(claims
                .FirstOrDefault(p => p.Type.Equals(ClaimTypes.NameIdentifier))
                .Value);
    }
}