using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EstateAgent.Business.Abstract;
using EstateAgent.Cache;
using EstateAgent.Dal.Concrete;
using EstateAgent.Entities;
using EstateAgent.MvcUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceStack.Redis;

namespace EstateAgent.MvcUI.Controllers
{
    public class AdminResidentialController : Controller
    {
        IResidentialService _service;
        IResidentialCache _cache;
        TimeSpan setExpiration = new TimeSpan(0, 2, 0);
        
        public AdminResidentialController(IResidentialService service, IResidentialCache cache)
        {
            _service = service;
            _cache = cache;
        }
        public ActionResult Index()
        {
            const string cacheKey = "ResidentialEstate*";
            var redisdata = _cache.GetFurnished(cacheKey);
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
        public ActionResult Create(ModelResidential model, IFormFile[] photos)
        {
            if (ModelState.IsValid)
            {
                var entity = new Residential()
                {
                    Title = model.Title,
                    Address = model.Address,
                    AgeOfBuilding = model.AgeOfBuilding,
                    Date = model.Date,
                    Description = model.Description,
                    FloorNumber = model.FloorNumber,
                    Furnished = model.Furnished,
                    NumberOfFloors = model.NumberOfFloors,
                    NumberOfRooms = model.NumberOfRooms,
                    Price = model.Price,
                    RealEstate = model.RealEstate,
                    Type = model.Type,
                    WithinaBuildingComplex = model.WithinaBuildingComplex
                };
                if (photos == null || photos.Length == 0)
                {
                    entity.Pictures = null;
                }
                else
                {
                    entity.Pictures = new List<string>();
                    foreach (var photo in photos)
                    {
                        var randomNames = Path.GetRandomFileName();
                        var fileName = Path.ChangeExtension(randomNames, ".jpg");
                        entity.Pictures.Add(fileName);
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                        var stream = new FileStream(path, FileMode.Create);
                        photo.CopyToAsync(stream);

                    }
                }
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
        public ActionResult Update(string id)
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
            var model = new ModelResidential()
            {
                Id = entity.Id.ToString(),
                Title = entity.Title,
                Address = entity.Address,
                AgeOfBuilding = entity.AgeOfBuilding,
                Date = entity.Date,
                Description = entity.Description,
                Furnished = entity.Furnished,
                NumberOfFloors = entity.NumberOfFloors,
                NumberOfRooms = entity.NumberOfRooms,
                Pictures = entity.Pictures,
                Price = entity.Price,
                RealEstate = entity.RealEstate,
                Type = entity.Type,
                WithinaBuildingComplex = entity.WithinaBuildingComplex

            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Update(ModelResidential model, IFormFile[] photos)
        {
            if (ModelState.IsValid)
            {
                var entity = _service.GetById(model.Id);
                if (entity == null)
                {
                    return null;
                }
                entity.Title = model.Title;
                entity.Address = model.Address;
                entity.AgeOfBuilding = model.AgeOfBuilding;
                entity.Date = model.Date;
                entity.Description = model.Description;
                entity.Furnished = model.Furnished;
                entity.NumberOfFloors = model.NumberOfFloors;
                entity.NumberOfRooms = model.NumberOfRooms;
                entity.Price = model.Price;
                entity.RealEstate = model.RealEstate;
                entity.Type = model.Type;
                entity.WithinaBuildingComplex = model.WithinaBuildingComplex;

                entity.Pictures = model.Pictures;


                _service.Update(model.Id, entity);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Remove(string id)
        {
            const string cacheKey = "ResidentialEstate";
            var entity = _service.GetById(id);
            if (entity == null)
            {
                return null;
            }
            _service.Delete(entity);
            _cache.Remove(cacheKey + id);
            return RedirectToAction("Index");
        }


    }
}