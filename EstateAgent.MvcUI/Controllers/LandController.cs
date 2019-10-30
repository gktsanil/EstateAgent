using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstateAgent.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace EstateAgent.MvcUI.Controllers
{
    public class LandController : Controller
    {
        ILandService _service;

        public LandController(ILandService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            var dataList = _service.GetAll();
            return View(dataList);
        }
    }
}