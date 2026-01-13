using MongoDB.Driver;
using Project_manager.Models;

namespace Project_manager.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _usersCollection;

        public UserService(IConfiguration configuration)
        {
            var connectionString = configuration["MongoDB:ConnectionString"];
            var databaseName = configuration["MongoDB:DatabaseName"];
            var collectionName = configuration["UserSettings:CollectionName"];

            if (string.IsNullOrEmpty(connectionString))
                throw new Exception("MongoDB connection string not found!");

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);

            _usersCollection = database.GetCollection<User>(collectionName);
        }

        public async Task<IEnumerable<User>> GetUsersAsync() =>

            await _usersCollection.Find(_ => true).ToListAsync();

        public async Task<User?> GetUserByIdAsync(string id) =>

            await _usersCollection.Find(u => u.Id == id).FirstOrDefaultAsync();

        public async Task AddUserAsync(User user) =>

            await _usersCollection.InsertOneAsync(user);

        public async Task UpdateUserAsync(string id, User updatedUser) =>

            await _usersCollection.ReplaceOneAsync(u => u.Id == id, updatedUser);

        public async Task DeleteUserAsync(string id) =>
        
            await _usersCollection.DeleteOneAsync(u => u.Id == id);
    }
}
