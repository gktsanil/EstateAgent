using EstateAgent.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EstateAgent.Cache
{
    public interface IResidentialCache : IRedisService<Residential>
    {
        List<Residential> GetFurnished(string cachekey);
        void Remove(string cachekey);
    }
}
