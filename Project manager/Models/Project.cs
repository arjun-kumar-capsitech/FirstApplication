using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Project_manager.Models
{
    public class Project
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("ProjectName")]
        public string ProjectName { get; set; } = string.Empty;

        [BsonElement("Description")]
        public string Description { get; set; } = string.Empty;

        [BsonElement("AssignUser")]
        public string AssignUser { get; set; } = string.Empty;

        [BsonElement("Status")]
        public string Status { get; set; } = "Todo";
    }
}
