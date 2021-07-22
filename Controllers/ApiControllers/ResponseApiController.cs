using Forum.Entities;
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
    [Route("api/category/{categoryid}/topic/{topicid}/response")]
    [ApiController]
    public class ResponseApiController : ControllerBase
    {
        private readonly IResponseService _responseService;
        public ResponseApiController(IResponseService responseService)
        {
            _responseService = responseService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Response>> GetAll([FromRoute] int topicid)
        {
            var responses = _responseService.Get(topicid);
            return Ok(responses);
        }

        [HttpGet("{id}")]
        public ActionResult<Response> GetById([FromRoute] int topicid, [FromRoute] int id)
        {
            var response = _responseService.GetById(topicid, id);
            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        public ActionResult CreateResponse([FromBody] CreateResponseDto dto, [FromRoute] int topicid, [FromRoute] int categoryid)
        {
            var responseId = _responseService.Create(dto, topicid);
            return Created($"/api/category/{categoryid}/topic/{topicid}/response/{responseId}", null);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin, Moderator")]
        public ActionResult DeleteResponse([FromRoute] int topicid, [FromRoute] int id)
        {
            _responseService.Delete(topicid,  id);
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateResponse([FromBody] CreateResponseDto dto, [FromRoute] int topicid, [FromRoute] int id)
        {
            _responseService.Update(dto, topicid, id);
            return Ok();
        }
    }
}
