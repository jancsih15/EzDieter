using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace EzDieter.Domain
{
    public class Day
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

    public class DayDish
    {
        //public Guid Id { get; set; }
        public string Name { get; set; }
        public string VolumeType { get; set; }
        public float Volume { get; set; }
    }
}