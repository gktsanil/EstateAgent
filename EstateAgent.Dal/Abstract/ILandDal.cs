using EstateAgent.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EstateAgent.Dal.Abstract
{
    public interface ILandDal:IRepository<Land>
    {
        Land Create(Land model);
        void Delete(Land model);
        void Delete(string id);
        Land GetById(string id);
        void Update(string id, Land model);
    }
}
