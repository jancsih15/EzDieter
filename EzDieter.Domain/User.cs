using System;
using System.Collections.Generic;
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

        public float TDEEs { get; set; }
    }
}