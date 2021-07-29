using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectPermanent("/Category");
        }
    }
}
