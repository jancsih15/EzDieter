using System;
using MongoDB.Bson.Serialization.Attributes;

namespace EzDieter.Domain
{
    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonId]
        public Guid Id { get; set; }
        
        [BsonElement("Username")]
        public string Username { get; set; } = null!;
        
        [BsonElement("PasswordHash")]
        public string PasswordHash { get; set; } = null!;
    }
}