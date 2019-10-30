using EstateAgent.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EstateAgent.Dal.Abstract
{
    public interface IResidentialDal:IRepository<Residential>
    {
        List<Residential> GetFirstThree();
        List<Residential> GetFurnished();
        Residential Create(Residential model);
        void Delete(Residential model);
        void Delete(string id);
        Residential GetById(string id);
        void Update(string id, Residential model);
    }
}
