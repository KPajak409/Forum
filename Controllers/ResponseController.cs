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
    public class ResponseController : Controller
    {
        private readonly IResponseService _responseService;

        public ResponseController(IResponseService responseService)
        {
            _responseService = responseService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Details(int topicid, int id)
        {
            var reponse = _responseService.GetById(topicid, id);
            return View(reponse);
        }


        [HttpPost("Create")]
        public ActionResult Create(CreateResponseDto dto, int categoryid, int topicid)
        {
            if (ModelState.IsValid)
            {
                _responseService.Create(dto, topicid);
                return RedirectToAction("Details", "Category", new { id = categoryid });
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
            return RedirectToAction("Details", "Category", new { id = categoryid });
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
        public ActionResult DeleteConfirmed(int topicid, int id)
        {

            _responseService.Delete(topicid, id);
            return RedirectToAction("Details", "Category", new { id = topicid });
        }
    }
}
