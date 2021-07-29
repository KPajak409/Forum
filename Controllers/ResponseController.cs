using Forum.Models;
using Forum.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Controllers
{
    [Route("Category/{categoryid}/Topic/{topicid}/Response")]
    [Authorize]
    public class ResponseController : Controller
    {
        private readonly IResponseService _responseService;
        private readonly ITopicService _topicService;

        public ResponseController(IResponseService responseService, ITopicService topicService)
        {
            _responseService = responseService;
            _topicService = topicService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Details(int topicid, int id)
        {
            var response = _responseService.GetById(topicid, id);
            return View(response);
        }


        [HttpPost("Create")]
        public ActionResult Create(CreateResponseDto dto, int categoryid, int topicid)
        {
            if (ModelState.IsValid)
            {
                var id = _responseService.Create(dto, topicid);
                return View("../Topic/Details", _topicService.GetById(categoryid, topicid));
            }
            return View();
        }

        [HttpGet("Edit/{id}")]
        public ActionResult Edit(int topicid, int? id)
        {
            var reponse = _responseService.GetById(topicid, (int)id);
            return View(reponse);
        }

        // POST: HomeController1/Edit/5
        [HttpPost("EditSave/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult EditSave(CreateResponseDto dto, int categoryid, int topicid, int id)
        {
            _responseService.Update(dto, topicid, id);
            return View("../Topic/Details", _topicService.GetById(categoryid, topicid));
        }

        [HttpGet("Delete/{id}")]
        public ActionResult Delete(int topicid, int id)
        {
            var reponse = _responseService.GetById(topicid, id);
            return View(reponse);
        }

        // POST: HomeController1/Delete/5
        [HttpPost("DeleteConfirmed/{id}")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int categoryid, int topicid, int id)
        {

            _responseService.Delete(topicid, id);
            return View("../Topic/Details", _topicService.GetById(categoryid, topicid));
        }
    }
}
