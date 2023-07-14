﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practical19_DataAccessLayer.Interfaces;
using Practical19_DataAccessLayer.Model;

namespace Practical19_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userRepository.RegisterUserAsync(model);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result.Message);
                }
            }
            return BadRequest("Properties are not valid");
        }
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userRepository.LoginUserAsync(model);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            return BadRequest("Properties are not valid");
        }
        [HttpPost("Logout")]
        public async Task<IActionResult> LogoutAsync(Logout model)
        {
            await _userRepository.LogoutUserAsync(model);
            return Ok();

        }

        [HttpGet("Users")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _userRepository.GetUsers();
            var ck = Request.Cookies;
            return Ok(data);
        }
    }
    }

