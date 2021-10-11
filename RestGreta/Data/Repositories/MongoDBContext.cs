using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using RestGreta.Data.Entities;


namespace RestGreta.Data.Repositories
{
    public class MongoDBContext
    {
        public IMongoDatabase mongodb;
        public MongoClient client;
        public MongoDBContext()
        {
            /*client = new MongoClient("mongodb + srv://admin:admin@cluster0.lmt7h.mongodb.net/saitynai?retryWrites=true&w=majority");
            mongodb = client.GetDatabase("saitynai");*/
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://admin:admin@cluster0.lmt7h.mongodb.net/saitynai?retryWrites=true&w=majority");
            client = new MongoClient(settings);
            mongodb = client.GetDatabase("saitynai");
        }
        public IMongoCollection<Comment> Comment
        {
            get
            {
                return mongodb.GetCollection<Comment>("comments");
            }
        }
        public IMongoCollection<Product> Product
        {
            get
            {
                return mongodb.GetCollection<Product>("products");
            }
        }
        public IMongoCollection<Recipe> Recipe
        {
            get
            {
                return mongodb.GetCollection<Recipe>("recipies");
            }
        }
        public IMongoCollection<UserList> UserList
        {
            get
            {
                return mongodb.GetCollection<UserList>("users");
            }
        }
    }
}
