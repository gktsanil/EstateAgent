using EstateAgent.Business.Abstract;
using EstateAgent.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EstateAgent.MvcUI.Controllers.api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ResidentialController : ControllerBase
    {
        IResidentialService _service;
        public ResidentialController(IResidentialService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<List<Residential>> Get()
        {
            return await _service.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<Residential> GetById(string id)
        {
            return await _service.GetByIdAsync(id);
        }

        [HttpGet]
        public async Task<List<Residential>> GetFirstThree()
        {
            return await _service.GetFirstThreeAsync();
        }

        [HttpGet]
        public async Task<List<Residential>> GetFurnished()
        {
            return await _service.GetFurnishedAsync();
        }        
    }
}