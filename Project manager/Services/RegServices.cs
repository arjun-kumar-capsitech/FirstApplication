using MongoDB.Driver;
using Project_manager.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;

namespace Project_manager.Services
{
    public class RegService
    {
        private readonly IMongoCollection<Reg> _regCollection;

        public RegService(IConfiguration configuration)
        {
            var connectionString = configuration["MongoDB:ConnectionString"];
            if (string.IsNullOrEmpty(connectionString))
                throw new Exception("MongoDb connection string not found!");

            var client = new MongoClient(connectionString);

            var databaseName = configuration["MongoDB:DatabaseName"];
            if (string.IsNullOrEmpty(databaseName))
                throw new Exception("MongoDb database name not found!");

            var database = client.GetDatabase(databaseName);

            var collectionName = configuration["RegSettings:CollectionName"];
            if (string.IsNullOrEmpty(collectionName))
                throw new Exception("MongoDb collection name not found!");

            _regCollection = database.GetCollection<Reg>(collectionName);
        }

        public async Task<List<Reg>> GetRegsAsync() =>
            await _regCollection.Find(_ => true).ToListAsync();

        public async Task<Reg?> GetRegByIdAsync(string id)
        {
            if (!ObjectId.TryParse(id, out _)) return null;
            return await _regCollection.Find(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddRegAsync(Reg reg) =>
            await _regCollection.InsertOneAsync(reg);

        public async Task UpdateRegAsync(string id, Reg updatedReg) =>
            await _regCollection.ReplaceOneAsync(r => r.Id == id, updatedReg);

        public async Task DeleteRegAsync(string id) =>
            await _regCollection.DeleteOneAsync(r => r.Id == id);
    }
}
