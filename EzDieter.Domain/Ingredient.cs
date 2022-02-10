using System;
using MongoDB.Bson.Serialization.Attributes;

namespace EzDieter.Domain
{
    public class Ingredient
    {
        [BsonId]
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        // Nutrition Data
        public float Calorie { get; set; }

        public float Carbohydrate { get; set; }

        public float Fat { get; set; }

        public float Protein { get; set; }

        // Quantity Data
        public string VolumeType { get; set; } = null!;

        public float Volume { get; set; }
        
    }
}