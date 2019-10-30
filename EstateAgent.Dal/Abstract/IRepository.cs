using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace EstateAgent.Dal.Abstract
{
    public interface IRepository<TModel>
    {
        List<TModel> GetAll();
    }
}
