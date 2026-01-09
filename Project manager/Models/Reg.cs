using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Project_manager.Models
{
    public class Reg
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("username")]
        public string Username { get; set; } = string.Empty;

        [BsonElement("email")]
        public string Email { get; set; } = string.Empty;

        [BsonElement("password")]
        public string Password { get; set; } = string.Empty;

        [BsonElement("confirmPassword")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
