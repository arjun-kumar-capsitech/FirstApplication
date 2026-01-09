using MongoDB.Bson.Serialization.Attributes;

namespace Project_manager.Models
{
    public class Userdto
    {
        [BsonElement("Name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("Email")]
        public string Email { get; set; } = string.Empty;

        [BsonElement("Role")]
        public string Role { get; set; } = string.Empty;
    }
}
