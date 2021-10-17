using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestGreta.Data.Entities;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc;

namespace RestGreta.Data.Repositories
{
    public interface IRecipesRepository
    {
        Task Create(Recipe recipe);
        Task Delete(string id);
        Task<Recipe> Get(string id);
        Task<IEnumerable<Recipe>> GetAll();
        Task Put(Recipe recipe);
        //hierarchija
        Task<IEnumerable<Comment>> GetAllComments(string id);
        Task<Comment> GetComment(string recipeId, string id);
        Task CreateComment(Comment recipe, string id);

        Task DeleteComment(string id, string commentId);
        Task PutComment(Comment recipe, string id, string commentId);
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
        //----------------------------------------------------------------------------------------
        public async Task<IEnumerable<Comment>> GetAllComments(string id)
        {
            try
            {
                var fil = Builders<RecipeComment>.Filter.Eq(x => x.RecipeId, id);

                var rez = db.RecipeComment.Find(fil).ToList();
                string[] ids = new string[rez.Count];
                var builder = Builders<Comment>.Filter;
                FilterDefinition<Comment>[] filtered = new FilterDefinition<Comment>[rez.Count];
                for (int i = 0; i < rez.Count; i++)
                {
                    ids[i] = rez[i].CommentId;
                    filtered[i] = builder.Eq(u => u.Id, ids[i]);

                }
                var newFil = builder.Or(filtered);

                return await db.Comment.Find(newFil).ToListAsync();

            }
            catch
            {

                return null;
            }
        }

        public async Task CreateComment(Comment recipe, string id)
        {
            try
            {
                FilterDefinition<Recipe> filter = Builders<Recipe>.Filter.Eq(s => s.Id, id);
                var recipeCom = new RecipeComment();
                await db.Comment.InsertOneAsync(recipe);

                recipeCom.RecipeId = id;
                recipeCom.CommentId = recipe.Id;
                await db.RecipeComment.InsertOneAsync(recipeCom);
            }
            catch
            {

                throw;
            }
        }
        public async Task<Comment> GetComment(string recipeId, string id)
        {
            try
            {
                var builder = Builders<Comment>.Filter;
                var builder0 = Builders<RecipeComment>.Filter;
                var filteredRecipes = builder0.Eq(x => x.RecipeId, recipeId);
                var recipes = db.RecipeComment.Find(filteredRecipes).ToList();
                FilterDefinition<Comment> filtered = null;
                foreach (var item in recipes)
                {
                    if (item.CommentId == id)
                    {
                        filtered = builder.Eq(x => x.Id, id);
                    }
                }

                return await db.Comment.Find(filtered).FirstOrDefaultAsync();

            }
            catch
            {

                return null;
            }
        }
        public async Task PutComment(Comment recipe, string id, string commentId)
        {
            try
            {
                var builder = Builders<Comment>.Filter;
                var builder0 = Builders<RecipeComment>.Filter;
                var filteredPosts = builder0.Eq(x => x.RecipeId, id);
                var recipes = db.RecipeComment.Find(filteredPosts).ToList();
                FilterDefinition<Comment> filtered = null;
                foreach (var item in recipes)
                {
                    if (item.CommentId == commentId)
                    {
                        filtered = builder.Eq(x => x.Id, commentId);
                    }
                }

                await db.Comment.ReplaceOneAsync(filtered, recipe);
            }
            catch
            {

                throw;
            }
        }
        public async Task DeleteComment(string id, string commentId)
        {
            try
            {
                var builder = Builders<Comment>.Filter;
                var builder0 = Builders<RecipeComment>.Filter;
                var filteredRecipes = builder0.Eq(x => x.RecipeId, id);
                var recipes = db.RecipeComment.Find(filteredRecipes).ToList();
                FilterDefinition<Comment> filtered = null;
                foreach (var item in recipes)
                {
                    if (item.CommentId == commentId)
                    {
                        filtered = builder.Eq(x => x.Id, commentId);
                    }
                }

                await db.Comment.DeleteOneAsync(filtered);
            }
            catch
            {

                throw;
            }
        }
    }
}
