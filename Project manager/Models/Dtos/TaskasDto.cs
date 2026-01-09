using MongoDB.Bson.Serialization.Attributes;
namespace Project_manager.Models
{
    public class TaskasDto
    {
       
        [BsonElement("Title")]
        public string Title { get; set; } = string.Empty;
        [BsonElement("Description")]
        public string Description { get; set; } = string.Empty;
        [BsonElement("Project")]
        public string Project { get; set; } = string.Empty;
        [BsonElement("Status")]
        public string Status { get; set; } = "Todo";

    }

}