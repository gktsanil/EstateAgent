using System;
using System.Collections.Generic;
using System.Text;

namespace EstateAgent.Cache
{
    public interface IRedisService<T> where T:class
    {
        List<T> GetAll(string cachekey);
        T GetById(string cachekey);
        void Set(string id, object value,TimeSpan time);
    }
}
