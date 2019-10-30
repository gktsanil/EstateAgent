using EstateAgent.Business.Abstract;
using EstateAgent.Dal.Abstract;
using EstateAgent.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EstateAgent.Business.Concrete
{
    public class ResidentialManager : IResidentialService
    {
        private IResidentialDal _residentialDal;
        
        public ResidentialManager(IResidentialDal residentialDal)
        {
            _residentialDal = residentialDal;
        }

        public Residential Create(Residential model)
        {
            return _residentialDal.Create(model);
        }

        public void Delete(Residential model)
        {
            _residentialDal.Delete(model);
        }

        public void Delete(string id)
        {
            _residentialDal.Delete(id);
        }

        public List<Residential> GetAll()
        {
            return _residentialDal.GetAll();
        }

        public Residential GetById(string id)
        {
            return _residentialDal.GetById(id);
        }

        public List<Residential> GetFirstThree()
        {
            return _residentialDal.GetFirstThree();
        }

        public List<Residential> GetFurnished()
        {
            return _residentialDal.GetFurnished();
        }

        public void Update(string id, Residential model)
        {
            _residentialDal.Update(id, model);
        }
    }
}
