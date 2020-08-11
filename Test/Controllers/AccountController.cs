using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Services.Dto;
using Services.UserServices;
using Test.Models.UserModels;

namespace Test.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (registerDto == null)
                return BadRequest("اطلاعات کامل نیست");

            var user = await _userService.RegisterAsync(registerDto);

            return Ok("عملیات با موفقیت انجام شد");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var calims = User.Claims.ToList();
            var data = await _userService.GetAllUsersAsync();
            var result = _userService

            return Ok(await _userService.GetAllUsersAsync());
        }
    }
}