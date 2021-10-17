using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestGreta.Data.Entities
{
    public class RecipeComment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("RecipeId")]
        public string RecipeId { get; set; }

        [BsonElement("CommentId")]
        public string CommentId { get; set; }
    }
}
