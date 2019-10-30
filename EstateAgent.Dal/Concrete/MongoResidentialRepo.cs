using EstateAgent.Dal.Abstract;
using EstateAgent.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace EstateAgent.Dal.Concrete
{
    public class MongoResidentialRepo : IResidentialDal
    {
        private IMongoCollection<Residential> _mongoResidentialCollection;
        public MongoResidentialRepo(string mongoDBConnectionString="mongodb://localhost:27017", string dbName= "EstateAgentDb", string collectionName="Residential")//Forward from startup
        {
            var client = new MongoClient(mongoDBConnectionString);
            var database = client.GetDatabase(dbName);
            _mongoResidentialCollection = database.GetCollection<Residential>(collectionName);
        }

        public Residential Create(Residential model)
        {
            _mongoResidentialCollection.InsertOne(model);
            return model;
        }

        public void Delete(Residential model)
        {
            _mongoResidentialCollection.DeleteOne(m => m.Id == model.Id);
        }

        public void Delete(string id)
        {
            var docId = new ObjectId(id);
            _mongoResidentialCollection.DeleteOne(m => m.Id == docId);
        }

        public List<Residential> GetAll()
        {
            return _mongoResidentialCollection.Find(x => true).ToList();
        }
        public List<Residential> GetFurnished()
        {
            return _mongoResidentialCollection.Find(m => m.Furnished==true).ToList();
        }

        public Residential GetById(string id)
        {
            var docId = new ObjectId(id);
            return _mongoResidentialCollection.Find<Residential>(m => m.Id == docId).FirstOrDefault();
        }

        public List<Residential> GetFirstThree()
        {
            return _mongoResidentialCollection.Find(x => true).Limit(3).ToList();
        }

        public void Update(string id, Residential model)
        {
            var docId = new ObjectId(id);
            _mongoResidentialCollection.ReplaceOne(m => m.Id == docId, model);
        }
    }
}
