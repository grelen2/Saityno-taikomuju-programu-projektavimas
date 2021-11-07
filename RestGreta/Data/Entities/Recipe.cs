using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace RestGreta.Data.Entities
{
    public class Recipe
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required]
        [BsonElement("RecipeName")]
        public string RecipeName { get; set; }
        [Required]
        [BsonElement("ProductName")]
        public string ProductName { get; set; }
        [Required]
        [BsonElement("Quantity")]
        public string  Quantity { get; set; }
        [Required]
        [BsonElement("SeesUnits")]
        public string SeesUnits { get; set; }
        [Required]
        [BsonElement("Description")]
        public string Description { get; set; }

    }
}
