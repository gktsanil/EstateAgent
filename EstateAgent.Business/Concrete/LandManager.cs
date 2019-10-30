using EstateAgent.Business.Abstract;
using EstateAgent.Dal.Abstract;
using EstateAgent.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EstateAgent.Business.Concrete
{
    public class LandManager : ILandService
    {
        private ILandDal _landDal;
        public LandManager(ILandDal landDal)
        {
            _landDal = landDal;
        }
        public Land Create(Land model)
        {
            return _landDal.Create(model);
        }

        public void Delete(Land model)
        {
            _landDal.Delete(model);
        }

        public void Delete(string id)
        {
            _landDal.Delete(id);
        }

        public List<Land> GetAll()
        {
            return _landDal.GetAll();
        }

        public Land GetById(string id)
        {
            return _landDal.GetById(id);
        }

        public void Update(string id, Land model)
        {
            _landDal.Update(id, model);
        }
    }
}
