using MongoDB.Driver;
using Project_manager.Models;

namespace Project_manager.Services
{
    public class ProjectService
    {
        private readonly IMongoCollection<Project> _projects;

        public ProjectService(IConfiguration config)
        {
            var connectionString = config["MongoDB:ConnectionString"];
            var databaseName = config["MongoDB:DatabaseName"];

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);

            _projects = database.GetCollection<Project>("projects");
        }

        public async Task<List<Project>> GetProjectsAsync() =>
            await _projects.Find(_ => true).ToListAsync();

        public async Task<Project?> GetProjectByIdAsync(string id) =>
            await _projects.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task AddProjectAsync(Project project) =>
            await _projects.InsertOneAsync(project);

        public async Task UpdateProjectAsync(string id, Project project) =>
            await _projects.ReplaceOneAsync(x => x.Id == id, project);

        public async Task DeleteProjectAsync(string id) =>
            await _projects.DeleteOneAsync(x => x.Id == id);
    }
}
