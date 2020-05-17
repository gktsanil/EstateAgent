using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstateAgent.Business;
using EstateAgent.Business.Abstract;
using EstateAgent.Cache;
using EstateAgent.MvcUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EstateAgent.MvcUI.Controllers
{
    public class ResidentialController : Controller
    {
        IResidentialService _service;
        public ResidentialController(IResidentialService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            var dataList =  _service.GetAll();
            return View(dataList);
        }

        public IActionResult Details(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var residential = _service.GetById(id);
                if (residential != null)
                {
                    var model = new ModelResidentialDetails()
                    {
                        Residential = residential
                    };
                    return View(model);
                }
            }
            return NotFound();
        }
    }
}