using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace EzDieter.Domain
{
    public class Day2
    {
        [BsonId]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        
        public DateTime Date { get; set; }

        public List<DayDish> DayDishes { get; set; }
        
        // Nutrition Data
        public float Calorie { get; set; }

        public float Carbohydrate { get; set; }

        public float Fat { get; set; }

        public float Protein { get; set; }

        public float Weight { get; set; }
    }
}