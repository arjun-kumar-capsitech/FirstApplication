using MongoDB.Driver;
using Project_manager.Models;
using WebMOngodb.APIServices;

namespace Project_manager.Services
{
    public class TaskasService
    {
        private readonly IMongoCollection<Taskas> _taskasCollection;

        public TaskasService(MongodbServices mongo)
        {
            _taskasCollection = mongo.Database.GetCollection<Taskas>("Taskas");
        }

        public async Task<List<Taskas>> GetAllTaskasAsync()
        {
            return await _taskasCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Taskas?> GetTaskasByIdAsync(string id)
        {
            return await _taskasCollection.Find(t => t.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddTaskasAsync(Taskas task)
        {
            await _taskasCollection.InsertOneAsync(task);
        }

        public async Task UpdateTaskasAsync(string id, Taskas updated)
        {
            await _taskasCollection.ReplaceOneAsync(t => t.Id == id, updated);
        }

        public async Task DeleteTaskasAsync(string id)
        {
            await _taskasCollection.DeleteOneAsync(t => t.Id == id);
        }
    }
}
