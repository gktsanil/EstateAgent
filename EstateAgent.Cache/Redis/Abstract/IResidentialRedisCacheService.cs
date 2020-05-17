using EstateAgent.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EstateAgent.Cache.Redis.Abstract
{
    public interface IResidentialRedisCacheService : IRedisCacheService
    {
        void SetList(string cachekey, List<Residential> valueList, TimeSpan time);
    }
}
