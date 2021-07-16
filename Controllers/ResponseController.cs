﻿using Forum.Entities;
using Forum.Models;
using Forum.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Controllers
{
    [Route("api/category/{categoryid}/topic/{topicid}/response")]
    [ApiController]
    public class ResponseController : Controller
    {
        private readonly IResponseService _responseService;
        public ResponseController(IResponseService responseService)
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
        public ActionResult CreateTopic([FromBody] CreateResponseDto dto, [FromRoute] int topicid, [FromRoute] int categoryid)
        {
            var responseId = _responseService.Create(dto, topicid);
            return Created($"/api/category/{categoryid}/topic/{topicid}/response/{responseId}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTopic([FromRoute] int topicid, [FromRoute] int id)
        {
            _responseService.Delete(topicid,  id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTopic([FromBody] CreateResponseDto dto, [FromRoute] int topicid, [FromRoute] int id)
        {
            _responseService.Update(dto, topicid, id);
            return Ok();
        }
    }
}
