using Forum.Entities;
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
    [Route("Category/{categoryid}/Topic")]
    public class TopicController : Controller
    {
        private readonly ITopicService _topicService;
        public TopicController(ITopicService topicService)
        {
            _topicService = topicService;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Details(int categoryid, int id)
        {
            //var routeValues = Url.ActionContext.RouteData.Values["id"];
            var topic = _topicService.GetById(categoryid, id);
            return View(topic);
        }

        [HttpPost("Create")]
        public ActionResult Create(CreateTopicDto dto, int categoryid)
        {
            if (ModelState.IsValid)
            {
                _topicService.Create(dto, categoryid);
                return RedirectToAction("Details", "Category", new { id = categoryid });
            }
            return View();
        }

        [HttpGet("Edit/{id}")]
        public ActionResult Edit(int categoryid, int? id)
        {
            var topic = _topicService.GetById(categoryid, (int)id);
            return View(topic);
        }

        // POST: HomeController1/Edit/5
        [HttpPost("EditSave/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult EditSave(CreateTopicDto dto, int categoryid, int id)
        {
            _topicService.Update(dto, categoryid, id);
            return RedirectToAction("Details", "Category", new { id = categoryid });
        }

        // GET: HomeController1/Delete/5
        [HttpGet("Delete/{id}")]
        public ActionResult Delete(int categoryid, int id)
        {
            var topic = _topicService.GetById(categoryid, id);
            return View(topic);
        }

        // POST: HomeController1/Delete/5
        [HttpPost("DeleteConfirmed/{id}")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int categoryid, int id)
        {
            
            _topicService.Delete(categoryid, id);
            return RedirectToAction("Details", "Category", new { id = categoryid });
        }
    }
}
