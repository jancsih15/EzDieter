using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace EzDieter.Domain
{
    public class Dish
    {
        [BsonId]
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public List<DishIngredient> DishIngredients { get; set; }

        // Nutrition Data
        public float Calorie { get; set; }

        public float Carbohydrate { get; set; }

        public float Fat { get; set; }

        public float Protein { get; set; }

        // Quantity Data
        public string VolumeType { get; set; } = null!;

        public float Volume { get; set; }
    }

    public class DishIngredient
    {
        public string Name { get; set; }
        public string VolumeType { get; set; }
        public float Volume { get; set; }
    }
}