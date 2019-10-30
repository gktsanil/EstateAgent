using EstateAgent.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EstateAgent.Business.Abstract
{
    public interface IResidentialService
    {
        List<Residential> GetAll();
        List<Residential> GetFurnished();
        List<Residential> GetFirstThree();
        Residential Create(Residential model);
        void Delete(Residential model);
        void Delete(string id);
        Residential GetById(string id);
        void Update(string id, Residential model);
    }
}
