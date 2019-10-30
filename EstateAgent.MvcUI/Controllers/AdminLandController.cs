using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstateAgent.Business.Abstract;
using EstateAgent.Cache;
using EstateAgent.Dal.Concrete;
using EstateAgent.Entities;
using EstateAgent.MvcUI.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceStack.Redis;

namespace EstateAgent.MvcUI.Controllers
{
    public class AdminLandController : Controller
    {
        ILandService _service;
        ILandCache _cache;
        TimeSpan setExpiration = new TimeSpan(0, 5, 0);

        public AdminLandController(ILandService service, ILandCache cache)
        {
            _service = service;
            _cache = cache;
        }
        public IActionResult Index()
        {
            const string cacheKey = "Land*";
            var redisdata = _cache.GetAll(cacheKey);
            ViewBag.RedisData = redisdata;

            var dataList = _service.GetAll();
            return View(dataList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ModelLand model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Land()
                {
                    Title = model.Title,
                    Address = model.Address,
                    Date = model.Date,
                    Description = model.Description,
                    Pictures=model.Pictures,
                    Price = model.Price,
                    RealEstate = model.RealEstate,
                    Type = model.Type,
                    WithinaBuildingComplex = model.WithinaBuildingComplex
                };
                _service.Create(entity);
                _cache.Set(entity.Id.ToString(), entity, setExpiration);
                
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }

        }

        [HttpGet]
        public ActionResult Update(string? id)
        {
            if (id == null)
            {
                return null;
            }
            var entity = _service.GetById(id);
            if (entity == null)
            {
                return null;
            }
            var model = new ModelLand()
            {
                Id = entity.Id.ToString(),
                Title = entity.Title
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Update(ModelLand model)
        {
            if (ModelState.IsValid)
            {
                var entity = _service.GetById(model.Id);
                if (entity == null)
                {
                    return null;
                }
                entity.Title = model.Title;
                //entity.Address = model.Address;
                //entity.AgeOfBuilding = model.AgeOfBuilding;
                //entity.Date = model.Date;
                //entity.Description = model.Description;
                //entity.Furnished = model.Furnished;
                //entity.NumberOfFloors = model.NumberOfFloors;
                //entity.NumberOfRooms = model.NumberOfRooms;
                //entity.Pictures = model.Pictures;
                //entity.Price = model.Price;
                //entity.RealEstate = model.RealEstate;
                //entity.Type = model.Type;
                //entity.WithinaBuildingComplex = model.WithinaBuildingComplex;

                _service.Update(model.Id, entity);
                _cache.Set(entity.Id.ToString(), entity, setExpiration);


                //using (IRedisClient client = new RedisClient())
                //{
                //    if (client.SearchKeys("Land*").Count == 0)
                //    {
                //        var cachedata = client.As<Land>();
                //        cachedata.SetValue("Land" + entity.Id.ToString(), entity);
                //    }
                //}
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Remove(string id)
        {
            var entity = _service.GetById(id);
            if (entity == null)
            {
                return null;
            }
            _service.Delete(entity);
            return RedirectToAction("Index");
        }
    }
}