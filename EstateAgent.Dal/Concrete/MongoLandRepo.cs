using EstateAgent.Dal.Abstract;
using EstateAgent.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace EstateAgent.Dal.Concrete
{
    public class MongoLandRepo : ILandDal
    {
        private IMongoCollection<Land> _mongoLandCollection;
        public MongoLandRepo(string mongoDBConnectionString = "mongodb://localhost:27017", string dbName = "EstateAgentDb", string collectionName = "Land")//Forward from startup
        {
            var client = new MongoClient(mongoDBConnectionString);
            var database = client.GetDatabase(dbName);
            _mongoLandCollection = database.GetCollection<Land>(collectionName);
        }

        public Land Create(Land model)
        {
            _mongoLandCollection.InsertOne(model);
            return model;
        }

        public void Delete(Land model)
        {
            _mongoLandCollection.DeleteOne(m => m.Id == model.Id);
        }

        public void Delete(string id)
        {
            var docId = new ObjectId(id);
            _mongoLandCollection.DeleteOne(m => m.Id == docId);
        }

        public List<Land> GetAll()
        {
            return _mongoLandCollection.Find(x => true).ToList();
        }

        public Land GetById(string id)
        {
            var docId = new ObjectId(id);
            return _mongoLandCollection.Find<Land>(m => m.Id == docId).FirstOrDefault();
        }

        public void Update(string id, Land model)
        {
            var docId = new ObjectId(id);
            _mongoLandCollection.ReplaceOne(m => m.Id == docId, model);
        }
    }
}
