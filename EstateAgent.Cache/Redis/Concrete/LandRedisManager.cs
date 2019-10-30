using System;
using System.Collections.Generic;
using System.Text;
using EstateAgent.Entities;
using ServiceStack.Redis;

namespace EstateAgent.Cache
{
    public class LandRedisManager : ILandCache
    {
        public List<Land> GetAll(string cachekey)
        {
            throw new NotImplementedException();
        }

        public Land GetById(string cachekey)
        {
            throw new NotImplementedException();
        }

        public void Set(string id, object value, TimeSpan time)
        {
            throw new NotImplementedException();
        }
    }
}
