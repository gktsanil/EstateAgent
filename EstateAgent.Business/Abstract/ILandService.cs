using EstateAgent.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EstateAgent.Business.Abstract
{
    public interface ILandService
    {
        List<Land> GetAll();
        Land Create(Land model);
        void Delete(Land model);
        void Delete(string id);
        Land GetById(string id);
        void Update(string id, Land model);
    }
}
