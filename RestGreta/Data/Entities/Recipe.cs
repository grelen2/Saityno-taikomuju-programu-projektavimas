using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestGreta.Data.Entities
{
    public class Recipe
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("RecipeName")]
        public string RecipeName { get; set; }
        [BsonElement("ProductName")]
        public string ProductName { get; set; }
        [BsonElement("Quantity")]
        public string  Quantity { get; set; }
        [BsonElement("SeesUnits")]
        public string SeesUnits { get; set; }
        [BsonElement("Description")]
        public string Description { get; set; }

    }
}
