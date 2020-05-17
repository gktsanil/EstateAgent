using System;
using System.Collections.Generic;
using EstateAgent.Cache.Models;
using EstateAgent.Cache.Redis.Abstract;
using EstateAgent.Entities;
using ServiceStack.Redis;

namespace EstateAgent.Cache.Redis.Concrete
{
    public class ResidentialRedisCacheManager : RedisCacheManager, IResidentialRedisCacheService
    {
        public void SetList(string cachekey, List<Residential> valueList, TimeSpan time)
        {
            using (IRedisClient client = new RedisClient())
            {   
                client.Set(cachekey, valueList, time);
            }
        }

    }
}
