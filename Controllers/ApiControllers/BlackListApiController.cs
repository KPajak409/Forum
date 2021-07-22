using Forum.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Controllers
{

    [Route("/api/blacklist")]
    [ApiController]
    [Authorize(Roles = "Admin, Moderator")]
    public class BlackListApiController : ControllerBase
    {
        private readonly IBlackListService _blackListService;
        public BlackListApiController(IBlackListService blackListService)
        {
            _blackListService = blackListService;
        }

        [HttpGet]
        public ActionResult GetBlackList()
        {
            var blackList = _blackListService.Get();
            return Ok(blackList);
        }

        [HttpGet("{id}")]
        public ActionResult GetBanById([FromRoute] int id)
        {
            var blackList = _blackListService.GetBanById(id);
            return Ok(blackList);
        }
    }
}
