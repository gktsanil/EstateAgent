using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EstateAgent.Business.Abstract;
using EstateAgent.Cache;
using EstateAgent.Dal.Concrete;
using EstateAgent.Entities;
using EstateAgent.MvcUI.Helper;
using EstateAgent.MvcUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceStack.Redis;

namespace EstateAgent.MvcUI.Controllers
{
    public class AdminResidentialController : Controller
    {
        IResidentialService _service;
        public AdminResidentialController(IResidentialService service)
        {
            _service = service;
        }
        public async Task<ActionResult> Index()
        {
            var dataList = await _service.GetAllAsync();
            return View(dataList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ModelResidential model, IFormFile[] photos)
        {
            if (ModelState.IsValid)
            {
                var entity = new Residential()
                {
                    Title = model.Title,
                    Address = model.Address,
                    AgeOfBuilding = model.AgeOfBuilding,
                    Date = DateTime.Now,
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
                        var fileName = PictureHelper.GetRandomFileNameWithExtension();
                        var stream = PictureHelper.FileStreamOperation(fileName);
                        entity.Pictures.Add(fileName);
                        await photo.CopyToAsync(stream);
                    }
                }
                var isCreated = await _service.CreateAsync(entity);
                if (isCreated)
                    return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<ActionResult> Update(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var entity = await _service.GetByIdAsync(id);
                if(entity != null)
                {
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
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Update(ModelResidential model, IFormFile[] photos)
        {
            if (ModelState.IsValid)
            {
                var entity = await _service.GetByIdAsync(model.Id);
                if (entity != null)
                {
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

                    await _service.UpdateAsync(model.Id, entity);
                    return RedirectToAction("Index");
                }
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Remove(string id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null)
            {
                return null;
            }
            var isDeleted = await _service.DeleteAsync(id);
            if (isDeleted)
            {
                return RedirectToAction("Index");
            }
            return NotFound();
            
        }
    }
}