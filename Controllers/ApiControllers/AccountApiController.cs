using Forum.Models;
using Forum.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountApiController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountApiController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("{id}")]
        public ActionResult GetUserById(int userId)
        {
            var user = _accountService.GetById(userId);
            return Ok(user);
        }

        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            _accountService.RegisterUser(dto);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult LoginUser([FromBody]  LoginUserDto dto)
        {
            string token = _accountService.GenerateJwt(dto);
            return Ok(token);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateUser([FromBody] UpdateUserDto dto,[FromRoute] int userId)
        {
            _accountService.Update(dto, userId);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}")]
        public ActionResult ChangeUserRole([FromRoute]int id, [FromBody]int roleId)
        {
            _accountService.ChangeRole(id, roleId);
            return Ok();
        }

        [Authorize(Roles = "Admin, Moderator")]
        [HttpPost("{id}")]
        public ActionResult BanUser([FromRoute] int id, [FromBody] BanUserDto dto)
        {
            _accountService.BanUser(id, dto);
            return Ok();
        }

    }
}
