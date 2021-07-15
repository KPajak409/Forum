using Forum.Entities;
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
            var topics = _topicService.GetById(categoryid, id);
            return Ok(topics);
        }
    }
}
