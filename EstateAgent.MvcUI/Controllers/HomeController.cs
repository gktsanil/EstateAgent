using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EstateAgent.MvcUI.Models;

namespace EstateAgent.MvcUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
            //Residental ve Land popüler 3 ürün bas api JS
        }

    }
}
