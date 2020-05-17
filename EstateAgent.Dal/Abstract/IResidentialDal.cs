using EstateAgent.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EstateAgent.Dal.Abstract
{
    public interface IResidentialDal:IRepository<Residential>
    {
        Task<List<Residential>> GetFirstThreeAsync();
        Task<List<Residential>> GetFurnishedAsync();
        List<Residential> GetFirstThree();
        List<Residential> GetFurnished();
        
    }
}
