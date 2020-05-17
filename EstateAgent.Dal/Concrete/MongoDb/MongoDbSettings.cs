using EstateAgent.Dal.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace EstateAgent.Dal.Concrete.MongoDb
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public string Collection { get; set; }
    }
}
