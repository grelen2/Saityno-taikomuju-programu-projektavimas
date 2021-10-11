using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestGreta.Data.Entities;
using MongoDB.Driver;
using MongoDB.Bson;

namespace RestGreta.Data.Repositories
{
    public interface IRecipesRepository
    {
        Task Create(Recipe recipe);
        Task Delete(string id);
        Task<Recipe> Get(string id);
        Task<IEnumerable<Recipe>> GetAll();
        Task Put(Recipe recipe);
    }

    public class RecipesRepository : IRecipesRepository
    {
        internal MongoDBContext db = new MongoDBContext();

        public async Task<IEnumerable<Recipe>> GetAll()
        {
            try
            {
                return await db.Recipe.Find(_ => true).ToListAsync();
            }
            catch
            {
                return null;
            }
        
        }
        public async Task<Recipe> Get(string id)
        {
            try
            {
                FilterDefinition<Recipe> filter = Builders<Recipe>.Filter.Eq(s => s.Id, new string(id));
                return await db.Recipe.Find(filter).FirstOrDefaultAsync();
            }
            catch
            {

                return null;
            }

        }
        public async Task Create(Recipe recipe)
        {
            try
            {
                await db.Recipe.InsertOneAsync(recipe);

            }
            catch
            {

                throw;
            }
        }
        public async Task Put(Recipe recipe)
        {

            try
            {
                var filter = Builders<Recipe>
                    .Filter
                    .Eq(s => s.Id, recipe.Id);
                await db.Recipe.ReplaceOneAsync(filter, recipe);
            }
            catch
            {

                throw;
            }
        }
        public async Task Delete(String id)
        {
            try
            {
                var filter = Builders<Recipe>.Filter.Eq(s => s.Id, new string(id));
                await db.Recipe.DeleteOneAsync(filter);
            }
            catch
            {

                throw;
            }
        }
    }
}
