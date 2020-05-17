using EstateAgent.Cache.Models;
using EstateAgent.Entities;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace EstateAgent.Cache
{
    public class RedisCacheManager : IRedisCacheService
    {
        public List<T> GetList<T>(string cachekey)
        {
            using (IRedisClient client = new RedisClient())
            {
                try
                {
                    return client.Get<List<T>>(cachekey);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Something went wrong when getting datalist in GetList<T> method:"+e);
                    return null;
                }
              
            }
        }
        
        public T Get<T>(string cachekey)
        {
            using (IRedisClient client = new RedisClient())
            {
                var redisdata = client.Get<T>(cachekey);

                return redisdata;
            }
        }

        public void Set<T>(string cachekey, object value, TimeSpan time)
        {
            using (IRedisClient client = new RedisClient())
            {
               client.Set(cachekey, value, time);
            }
        }

        public void Remove<T>(string cachekey)
        {
            using (IRedisClient client = new RedisClient())
            {
                client.Remove(cachekey);
            }
        }

        public bool ContainsKey(string cachekey)
        {
            using (IRedisClient client = new RedisClient())
            {
                return client.ContainsKey(cachekey);
            }
        }

        public void Clear<T>(string cachekey)
        {
            if(ContainsKey(cachekey))
            {
                using (IRedisClient client = new RedisClient())
                {
                    client.ContainsKey(cachekey);
                }
            }
        }

        /* public void SetFurnished(List<Residential> residentials, TimeSpan time)
         {
             using (IRedisClient client = new RedisClient())
             {
                 for (int i = 0; i < residentials.Count; i++)
                 {
                     var entity = new ModelResidential()
                     {
                         Id = residentials[i].Id,
                         Address = residentials[i].Address,
                         AgeOfBuilding = residentials[i].AgeOfBuilding,
                         Date = residentials[i].Date,
                         Description = residentials[i].Description,
                         FloorNumber = residentials[i].FloorNumber,
                         Furnished = residentials[i].Furnished,
                         NumberOfFloors = residentials[i].NumberOfFloors,
                         NumberOfRooms = residentials[i].NumberOfRooms,
                         Pictures = residentials[i].Pictures,
                         Price = residentials[i].Price,
                         RealEstate = residentials[i].RealEstate,
                         Title = residentials[i].Title,
                         Type = residentials[i].Type,
                         WithinaBuildingComplex = residentials[i].WithinaBuildingComplex
                     };
                     client.Set("ResidentialEstate" + entity.Id, entity, time);
                 }

             }
         }

         public List<Residential> GetFurnished(string cachekey)
         {
             using (IRedisClient client = new RedisClient())
             {
                 List<Residential> dataList = new List<Residential>();
                 List<string> allKeys = client.SearchKeys(cachekey);
                 if (allKeys.Count() != 0)
                 {
                     foreach (string key in allKeys)
                     {
                         var residential = client.Get<Residential>(key);
                         if (residential.Furnished == true)
                         {
                             dataList.Add(residential);
                         }
                     }
                     return dataList;
                 }
                 else
                 {
                     TimeSpan expiration = new TimeSpan(0, 2, 0);
                     var furnishedList = _service.GetFurnished();
                     SetFurnished(furnishedList, expiration);
                     allKeys = client.SearchKeys(cachekey);
                     foreach (string key in allKeys)
                     {
                         var residential = client.Get<Residential>(key);
                         if (residential.Furnished == true)
                         {
                             dataList.Add(residential);
                         }
                     }
                     return dataList;
                 }
             }
         }*/
        


    }
}


