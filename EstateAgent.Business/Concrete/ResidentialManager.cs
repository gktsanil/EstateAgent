using EstateAgent.Business.Abstract;
using EstateAgent.Cache;
using EstateAgent.Cache.Redis.Abstract;
using EstateAgent.Dal.Abstract;
using EstateAgent.Entities;
using EstateAgent.Entities.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EstateAgent.Business.Concrete
{
    public class ResidentialManager : IResidentialService
    {
        private IResidentialDal _residentialDal;
        private IResidentialRedisCacheService _cache;
        public ResidentialManager(IResidentialDal residentialDal, IResidentialRedisCacheService cache)
        {
            _residentialDal = residentialDal;
            _cache = cache;
        }

        public async Task<bool> CreateAsync(Residential model)
        {
            return await _residentialDal.CreateAsync(model);
        }

        public void Delete(string id)
        {
            _residentialDal.Delete(id);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var cacheKey = $"{CacheKey.ResidentialCacheKey}{id}";
            var isDeleted = await _residentialDal.DeleteAsync(id);
            if (isDeleted)
            {
                _cache.Clear<Residential>(cacheKey);
                return true;
            }
            return false;
        }

        public List<Residential> GetAll()
        {
            return _residentialDal.GetAll();
        }

        public async Task<List<Residential>> GetAllAsync()
        {
            return await _residentialDal.GetAllAsync();
        }

        public Residential GetById(string id)
        {
            var cacheKey = $"{CacheKey.ResidentialCacheKey}{id}";
            if (_cache.ContainsKey(cacheKey))
            {
                return _cache.Get<Residential>(cacheKey);
            }
            else
            {
                var data = _residentialDal.GetById(id);
                if (data != null)
                {
                    _cache.Set<Residential>(cacheKey, data, TimeSpan.FromSeconds(CacheKey.Default_Expiration));
                    return data;
                }
                return null;
            }
        }

        public async Task<Residential> GetByIdAsync(string id)
        {
            var cacheKey = $"{CacheKey.ResidentialCacheKey}{id}";
            if (_cache.ContainsKey(cacheKey))
            {
                return _cache.Get<Residential>(cacheKey);
            }
            else
            {
                var data = await _residentialDal.GetByIdAsync(id);
                if (data != null)
                {
                    _cache.Set<Residential>(cacheKey, data, TimeSpan.FromSeconds(CacheKey.Default_Expiration));
                    return data;
                }
                return null;
            }
        }

        public List<Residential> GetFirstThree()
        {
            return _residentialDal.GetFirstThree();
        }

        public async Task<List<Residential>> GetFirstThreeAsync()
        {
            var cacheKey = $"{CacheKey.ResidentialCacheKey}get.first.three";
            if (_cache.ContainsKey(cacheKey))
            {
                return _cache.GetList<Residential>(cacheKey);

            }
            var residentialList = await _residentialDal.GetFirstThreeAsync();
            _cache.SetList(cacheKey, residentialList, TimeSpan.FromSeconds(CacheKey.Default_Expiration));
            return residentialList;
        }

        public List<Residential> GetFurnished()
        {
            return _residentialDal.GetFurnished();
        }

        public async Task<List<Residential>> GetFurnishedAsync()
        {
            var cacheKey = $"{CacheKey.ResidentialCacheKey}get.furnished";
            if (_cache.ContainsKey(cacheKey))
            {
                return _cache.GetList<Residential>(cacheKey);
            }
            var residentialList = await _residentialDal.GetFurnishedAsync();
            _cache.SetList(cacheKey, residentialList, TimeSpan.FromSeconds(CacheKey.Default_Expiration));
            return residentialList;
        }

        public async void Task<UpdateAsync>(string id, Residential model)
        {
           await _residentialDal.UpdateAsync(id, model);
        }

        public void Update(string id, Residential model)
        {
            _residentialDal.Update(id, model);
        }

        public async Task<Residential> UpdateAsync(string id, Residential model)
        {
            var cacheKey = $"{CacheKey.ResidentialCacheKey}{id}";
            if (_cache.ContainsKey(cacheKey))
            {
                _cache.Clear<Residential>(cacheKey);
            }
            return await _residentialDal.UpdateAsync(id, model);
        }
    }
}
