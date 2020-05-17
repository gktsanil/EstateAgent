using EstateAgent.Dal.Abstract;
using EstateAgent.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EstateAgent.Dal.Concrete.MongoDb
{
    public class ResidentialDal : IResidentialDal 
    {
        private IMongoCollection<Residential> _mongoResidentialCollection;
        public ResidentialDal(IMongoDbSettings settings)
        {
            MongoClient client = new MongoClient(settings.ConnectionString);
            var db = client.GetDatabase(settings.Database);
            _mongoResidentialCollection = db.GetCollection<Residential>(settings.Collection);
        }

        public async Task<bool> CreateAsync(Residential model)
        {
            try
            {
                await _mongoResidentialCollection.InsertOneAsync(model);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong: ", e);
                return false;
            }
        }

        public void Delete<t>(Residential model)
        {
            _mongoResidentialCollection.DeleteOne(m => m.Id == model.Id);
        }

        public void Delete(string id)
        {
            var docId = new ObjectId(id);
            _mongoResidentialCollection.DeleteOne(m => m.Id == docId);
        }
        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                var docId = new ObjectId(id);
                await _mongoResidentialCollection.DeleteOneAsync(m => m.Id == docId);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong", e);
            }
        }

        public List<Residential> GetAll()
        {
            return _mongoResidentialCollection.Find(x => true).ToList();
        }

        public async Task<List<Residential>> GetAllAsync()
        {
            return await _mongoResidentialCollection.Find(x => true).ToListAsync();
        }

        public List<Residential> GetFurnished()
        {
            return _mongoResidentialCollection.Find(m => m.Furnished==true).ToList();
        }
        public async Task<List<Residential>> GetFurnishedAsync()
        {
            return await _mongoResidentialCollection.Find(m => m.Furnished == true).ToListAsync();
        }

        public Residential GetById(string id)
        {
            try
            {
                var docId = new ObjectId(id);
                return _mongoResidentialCollection.Find<Residential>(m => m.Id == docId).FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine("Someting went wrong: ", e);
                return null;
            }
           
        }
        public async Task<Residential> GetByIdAsync(string id)
        {
            try
            {
                var docId = new ObjectId(id);
                return await _mongoResidentialCollection.Find<Residential>(m => m.Id == docId).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("Someting went wrong: ", e);
                return null;
            }
        }

        public List<Residential> GetFirstThree()
        {
            return _mongoResidentialCollection.Find(x => true).Limit(3).ToList();
        }
        public async Task<List<Residential>> GetFirstThreeAsync()
        {
            return await _mongoResidentialCollection.Find(x => true).Limit(3).ToListAsync();
        }
        public void Update(string id, Residential model)
        {
            var docId = new ObjectId(id);
            _mongoResidentialCollection.ReplaceOne(m => m.Id == docId, model);
        }

        public async Task<Residential> UpdateAsync(string id, Residential model)
        {
            var docId = new ObjectId(id);
            await _mongoResidentialCollection.ReplaceOneAsync(m => m.Id == docId, model);
            return await GetByIdAsync(id);
        }
    }
}
