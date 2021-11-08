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

            //var settings = MongoClientSettings.FromConnectionString("mongodb+srv://admin:admin@cluster0.lmt7h.mongodb.net/saitynai?retryWrites=true&w=majority");
            var settings = MongoClientSettings.FromConnectionString("mongodb://recipiesportaldatabase:JFj2qX3mgWRY4H3SmsqcNH79aMiVK44dVzXk4RLVvEsxglaOpuJem2chTnRryU2sVbl61IfU1ealXIuNk4St1w==@recipiesportaldatabase.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@recipiesportaldatabase@");
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
        public IMongoCollection<User> User
        {
            get
            {
                return mongodb.GetCollection<User>("users");
            }
        }
        public IMongoCollection<RecipeComment> RecipeComment
        {
            get
            {
                return mongodb.GetCollection<RecipeComment>("RecipeComment");
            }
        }
        public IMongoCollection<UserRecipe> UserRecipe
        {
            get
            {
                return mongodb.GetCollection<UserRecipe>("userRecipe");
            }
        }
        public IMongoCollection<UserProduct> UserProduct
        {
            get
            {
                return mongodb.GetCollection<UserProduct>("userProduct");
            }
        }
        public IMongoCollection<UserList> UserList
        {
            get
            {
                return mongodb.GetCollection<UserList>("userList");
            }
        }
    }
}
