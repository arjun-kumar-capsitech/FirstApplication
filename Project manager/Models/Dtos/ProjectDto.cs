using MongoDB.Bson.Serialization.Attributes;

namespace Project_manager.Models
{
    public class ProjectDto
    {
        [BsonElement("ProjectName")]
        public string ProjectName { get; set; } = string.Empty;

        [BsonElement("Description")]
        public string Description { get; set; } = string.Empty;

        [BsonElement("AssignUser")]
        public string AssignUser { get; set; } = string.Empty;
    }
}
