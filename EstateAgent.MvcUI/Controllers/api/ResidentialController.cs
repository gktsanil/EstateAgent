using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstateAgent.Business.Abstract;
using EstateAgent.Cache;
using EstateAgent.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EstateAgent.MvcUI.Controllers.api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ResidentialController : ControllerBase
    {
        IResidentialService _service;
        IResidentialCache _cache;
        public ResidentialController(IResidentialService service, IResidentialCache cache)
        {
            _service = service;
            _cache = cache;
        }

        [HttpGet]
        public List<Residential> GetList()
        {
            var dataList = this._service.GetAll();
            return dataList;
        }

        [HttpGet]
        public List<Residential> GetFirstThree()
        {
            var dataList = this._service.GetFirstThree();
            return dataList;
        }

        [HttpGet]
        public List<Residential> GetFurnished()
        {
            const string cacheKey = "ResidentialEstate*";
            //var furnishedList = _service.GetFurnished();
            var furnishedList = _cache.GetFurnished(cacheKey);
            return furnishedList;
        }

        
    }
}