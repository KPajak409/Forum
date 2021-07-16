using Forum.Entities;
using Forum.Models;
using Forum.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Controllers
{
    [Route("api/category/{categoryid}/topic")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly ITopicService _topicService;
        public TopicController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Topic>> GetAll([FromRoute] int categoryid)
        {
            var topics = _topicService.Get(categoryid);
            return Ok(topics);
        }

        [HttpGet("{id}")]
        public ActionResult<Topic> GetById([FromRoute] int categoryid, [FromRoute] int id)
        {
            var topic = _topicService.GetById(categoryid, id);
            return Ok(topic);
        }

        [HttpPost]
        public ActionResult CreateTopic([FromBody] CreateTopicDto dto, [FromRoute] int categoryid)
        {
            var topicId = _topicService.Create(dto, categoryid);
            return Created($"/api/category/{categoryid}/topic/{topicId}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTopic([FromRoute] int categoryid, [FromRoute] int id)
        {
            _topicService.Delete(categoryid, id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTopic([FromBody] CreateTopicDto dto, [FromRoute] int categoryid, [FromRoute] int id)
        {
            _topicService.Update(dto, categoryid, id);
            return Ok();
        }
    }
}
