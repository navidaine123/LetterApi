using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Dto;
using Services.UserServices;

namespace Test.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LogInController : ControllerBase
    {
        private readonly IUserService _userService;

        public LogInController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> LogInAsync(LogInDto user)
        {
            try
            {
                LogInResponseDto res = await _userService.GenerateTokenAsync(user);
                if (res != null)
                    return Ok(res);

                return BadRequest("رمز یا گذرواژه غلط");
            }
            catch (Exception e)
            {
                return Unauthorized(e);
            }
        }
    }
}