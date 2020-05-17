using EstateAgent.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EstateAgent.Business.Abstract
{
    public interface IResidentialService
    {
        Task<List<Residential>> GetAllAsync();
        Task<bool> CreateAsync(Residential model);
        Task<Residential> GetByIdAsync(string id);
        Task<Residential> UpdateAsync(string id, Residential model);
        Task<List<Residential>> GetFirstThreeAsync();
        Task<List<Residential>> GetFurnishedAsync();
        List<Residential> GetAll();
        Task<bool> DeleteAsync(string id);
        List<Residential> GetFurnished();
        List<Residential> GetFirstThree();
        void Delete(string id);
        Residential GetById(string id);
        void Update(string id, Residential model);
    }
}
