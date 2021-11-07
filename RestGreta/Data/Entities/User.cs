using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace RestGreta.Data.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("UserName")]
        public string UserName { get; set; }
        [BsonElement("Password")]
        public string Password { get; set; }
        [BsonElement("Name")]
        public string Name{ get; set; }
        [BsonElement("Surname")]
        public string Surname { get; set; }
        [BsonElement("Address")]
        public string Address { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("BirthDate")]
        public DateTime BirthDate { get; set; }
        public string Role { get; set; }

    }
}
