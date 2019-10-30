using EstateAgent.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EstateAgent.Cache
{
    public interface ILandCache:IRedisService<Land>
    {
    }
}
