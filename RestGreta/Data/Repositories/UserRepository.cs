using System;
using System.Collections.Generic;
using System.Linq;
using RestGreta.Data.Entities;
using RestGreta.Data;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;


namespace RestGreta.Data.Repositories
{
    public interface IUserRepository
    {
        Task CreateProduct(Product product, string userId);
        Task CreateRecipe(Recipe recipe, string userId);
        Task CreateRecipeComment(Comment comment, string userId, string recipeId);
        Task CreatUser(User user);
        Task DeleteProduct(string userId, string productId);
        Task DeleteRecipe(string userId, string recipeId);
        Task DeleteRecipeComment(string userId, string recipeId, string commentId);
        Task DeleteUser(string id);
        Task<IEnumerable<User>> GetAllUsers();
        Task<IEnumerable<Product>> GetProducts(string userId);
        Task<Product> GetProduct(string userId, string productId);
        Task<Recipe> GetRecipe(string userId, string recipeId);
        Task<Comment> GetRecipeComment(string userId, string recipeId, string commentId);
        Task<IEnumerable<Comment>> GetRecipeComments(string userId, string recipeId);
        Task<IEnumerable<Recipe>> GetRecipes(string userId);
        Task<User> GetUser(string id);
        Task<User> GetUserEmail(string email);
        Task PutUser(User user);
        Task UpdateProduct(Product product, string userId, string productId);
        Task UpdateRecipe(Recipe recipe, string userId, string recipeId);
        Task UpdateRecipeComment(Comment recipe, string userId, string recipeId, string commentId);
    }

    public class UserRepository : IUserRepository
    {
        internal MongoDBContext db = new MongoDBContext();
        private IMongoCollection<User> collection;
        public UserRepository()
        {
            collection = db.mongodb.GetCollection<User>("Users");
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                return await db.User.Find(_ => true).ToListAsync();
            }
            catch
            {
                return null;
            }
        }
        public async Task<User> GetUser(string id)
        {
            try
            {
                FilterDefinition<User> filter = Builders<User>.Filter.Eq(s => s.Id, new string(id));
                return await db.User.Find(filter).FirstOrDefaultAsync();
            }
            catch
            {

                return null;
            }

        }
        public async Task<User> GetUserEmail(string email)
        {
            try
            {
                FilterDefinition<User> filter = Builders<User>.Filter.Eq(s => s.Email, new string(email));
                return await db.User.Find(filter).FirstOrDefaultAsync();
            }
            catch
            {

                return null;
            }

        }
        public async Task CreatUser(User user)
        {
            try
            {
                await db.User.InsertOneAsync(user);

            }
            catch
            {

                throw;
            }
        }
        public async Task PutUser(User user)
        {
            try
            {
                var filter = Builders<User>
                    .Filter
                    .Eq(s => s.Id, user.Id);
                await db.User.ReplaceOneAsync(filter, user);
            }
            catch
            {

                throw;
            }
        }
        public async Task DeleteUser(string id)
        {

            try
            {
                var filter = Builders<User>.Filter.Eq(s => s.Id, new string(id));
                await db.User.DeleteOneAsync(filter);
            }
            catch
            {

                throw;
            }
        }
        //----------------------------------------------Recipe-------------------
        public async Task<IEnumerable<Recipe>> GetRecipes(string userId)
        {
            try
            {
                return await db.Recipe.Find(_ => true).ToListAsync();
            }
            catch
            {
                return null;
            }
            /*try
            {
                var fil = Builders<UserRecipe>.Filter.Eq(x => x.UserId, userId);

                var rez = db.UserRecipe.Find(fil).ToList();
                if (rez.Count == 0)
                {
                    return null;
                }
                var builder = Builders<Recipe>.Filter;
                FilterDefinition<Recipe>[] filtered = new FilterDefinition<Recipe>[rez.Count];
                for (int i = 0; i < rez.Count; i++)
                {
                    filtered[i] = builder.Eq(u => u.Id, rez[i].RecipeId);

                }
                var newFil = builder.Or(filtered);

                return await db.Recipe.Find(newFil).ToListAsync();
            }
            catch
            {

                return null;
            }*/
        }
        public async Task CreateRecipe(Recipe recipe, string userId)
        {
            try
            {
                var recipeCom = new UserRecipe();
                await db.Recipe.InsertOneAsync(recipe);

                recipeCom.UserId = userId;
                recipeCom.RecipeId = recipe.Id;
                await db.UserRecipe.InsertOneAsync(recipeCom);
            }
            catch
            {

                throw;
            }
        }

        public async Task<Recipe> GetRecipe(string userId, string recipeId)
        {

            try
            {
                FilterDefinition<Recipe> filter = Builders<Recipe>.Filter.Eq(s => s.Id, new string(recipeId));
                return await db.Recipe.Find(filter).FirstOrDefaultAsync();
            }
            catch
            {

                return null;
            }
            /*try
            {
                var builder = Builders<Recipe>.Filter;
                var builder0 = Builders<UserRecipe>.Filter;
                var filteredRecipes = builder0.Eq(x => x.UserId, userId);
                var recipe = db.UserRecipe.Find(filteredRecipes).ToList();
                FilterDefinition<Recipe> filtered = null;
                foreach (var item in recipe)
                {
                    if (item.RecipeId == recipeId)
                    {
                        filtered = builder.Eq(x => x.Id, recipeId);
                    }
                }

                return await db.Recipe.Find(filtered).FirstOrDefaultAsync();
            }
            catch
            {

                return null;
            }*/
        }

        public async Task DeleteRecipe(string userId, string recipeId)
        {


            try
            {
                var builder = Builders<Recipe>.Filter;
                var builder0 = Builders<UserRecipe>.Filter;
                var filteredRecipes = builder0.Eq(x => x.UserId, userId);
                var recipes = db.UserRecipe.Find(filteredRecipes).ToList();
                FilterDefinition<Recipe> filtered = null;
                foreach (var item in recipes)
                {
                    if (item.RecipeId == recipeId)
                    {
                        filtered = builder.Eq(x => x.Id, recipeId);
                    }
                }
                await db.Recipe.DeleteOneAsync(filtered);
            }
            catch
            {

                throw;
            }
        }
        public async Task UpdateRecipe(Recipe recipe, string userId, string recipeId)
        {
            try
            {
                var builder = Builders<Recipe>.Filter;
                var builder0 = Builders<UserRecipe>.Filter;
                var filteredRecipes = builder0.Eq(x => x.UserId, userId);
                var recipes = db.UserRecipe.Find(filteredRecipes).ToList();
                FilterDefinition<Recipe> filtered = null;
                foreach (var item in recipes)
                {
                    if (item.RecipeId == recipeId)
                    {
                        filtered = builder.Eq(x => x.Id, recipeId);
                    }
                }

                await db.Recipe.ReplaceOneAsync(filtered, recipe);
            }
            catch
            {

                throw;
            }
        }
        //--------------------recipe comments-----------------
        public async Task<IEnumerable<Comment>> GetRecipeComments(string userId, string recipeId)
        {
            try
            {
                return await db.Comment.Find(_ => true).ToListAsync();
            }
            catch
            {
                return null;
            }
            /*try
            {
                var builder = Builders<Recipe>.Filter;
                var builder0 = Builders<UserRecipe>.Filter;
                var builderpc = Builders<RecipeComment>.Filter;
                var bulderc = Builders<Comment>.Filter;
                //filter users
                var filteredRecipes = builder0.Eq(x => x.UserId, userId);
                var recipes = db.UserRecipe.Find(filteredRecipes).ToList();
                if (recipes.Count == 0)
                {
                    return null;
                }
                FilterDefinition<Recipe> filtered = null;


                foreach (var item in recipes)
                {
                    if (item.RecipeId == recipeId)
                    {
                        filtered = builder.Eq(x => x.Id, recipeId);
                    }
                }
                if (filtered == null)
                {
                    return null;
                }
                var recipe = db.Recipe.Find(filtered).ToList();
                if (recipe.Count == 0)
                {
                    return null;
                }
                var filRecipes = builderpc.Eq(x => x.RecipeId, recipe[0].Id);
                var comms = db.RecipeComment.Find(filRecipes).ToList();
                if (comms.Count == 0)
                {
                    return null;
                }
                FilterDefinition<Comment>[] filtered2 = new FilterDefinition<Comment>[comms.Count];
                for (int i = 0; i < comms.Count; i++)
                {
                    if (comms[i].RecipeId == recipeId)
                    {
                        filtered2[i] = bulderc.Eq(x => x.Id, comms[i].CommentId);
                    }
                }
                if (filtered2.Length == 0)
                {
                    return null;
                }
                var final = bulderc.Or(filtered2);

                return await db.Comment.Find(final).ToListAsync();

            }
            catch
            {

                return null;
            }*/
        }
        public async Task CreateRecipeComment(Comment comment, string userId, string recipeId)
        {
            try
            {
                var userRecipe = new UserRecipe();
                await db.Comment.InsertOneAsync(comment);

                userRecipe.UserId = userId;
                userRecipe.RecipeId = recipeId;
                await db.UserRecipe.InsertOneAsync(userRecipe);

                var recipeCom = new RecipeComment();
                recipeCom.RecipeId = recipeId;
                recipeCom.CommentId = comment.Id;
                await db.RecipeComment.InsertOneAsync(recipeCom);

            }
            catch
            {

                throw;
            }
        }
        public async Task<Comment> GetRecipeComment(string userId, string recipeId, string commentId)
        {
            try
            {
                FilterDefinition<Comment> filter = Builders<Comment>.Filter.Eq(s => s.Id, new string(commentId));
                return await db.Comment.Find(filter).FirstOrDefaultAsync();
            }
            catch
            {

                return null;
            }
            /*try
            {
                var builder = Builders<Recipe>.Filter;
                var builderCom = Builders<Comment>.Filter;
                var builderPC = Builders<RecipeComment>.Filter;
                var builderUP = Builders<UserRecipe>.Filter;

                var filterUser = builderUP.Eq(x => x.UserId, userId);
                var users = db.UserRecipe.Find(filterUser).ToList();
                FilterDefinition<Recipe>[] filtered = new FilterDefinition<Recipe>[users.Count];
                for (int i = 0; i < users.Count; i++)
                {
                    if (users[i].UserId == userId)
                    {
                        filtered[i] = builder.Eq(x => x.Id, recipeId);
                    }
                }
                var filter = builder.Or(filtered);

                var recipe = db.Recipe.Find(filter).ToList();
                FilterDefinition<RecipeComment>[] filtered0 = new FilterDefinition<RecipeComment>[recipe.Count];
                for (int i = 0; i < recipe.Count; i++)
                {
                    filtered0[i] = builderPC.Eq(x => x.RecipeId, recipe[i].Id);
                    filtered0[i] = builderPC.Eq(x => x.RecipeId, recipe[i].Id);

                }
                var recipeFilter = builderPC.Or(filtered0);

                var comms = db.RecipeComment.Find(recipeFilter).ToList();

                FilterDefinition<Comment>[] filteredC = new FilterDefinition<Comment>[comms.Count];
                for (int i = 0; i < comms.Count; i++)
                {
                    if (comms[i].RecipeId == recipeId)
                    {
                        filteredC[i] = builderCom.Eq(x => x.Id, commentId);
                    }
                }

                var filterFin = builderCom.Or(filteredC);

                return await db.Comment.Find(filterFin).FirstOrDefaultAsync();


            }
            catch
            {

                return null;
            }*/
        }
        public async Task DeleteRecipeComment(string userId, string recipeId, string commentId)
        {
            try
            {
                var builder = Builders<Recipe>.Filter;
                var builderCom = Builders<Comment>.Filter;
                var builderPC = Builders<RecipeComment>.Filter;
                var builderUP = Builders<UserRecipe>.Filter;

                var filterUser = builderUP.Eq(x => x.UserId, userId);
                var users = db.UserRecipe.Find(filterUser).ToList();
                FilterDefinition<Recipe>[] filtered = new FilterDefinition<Recipe>[users.Count];
                for (int i = 0; i < users.Count; i++)
                {
                    if (users[i].UserId == userId)
                    {
                        filtered[i] = builder.Eq(x => x.Id, recipeId);
                    }
                }
                var filter = builder.Or(filtered);

                var posts = db.Recipe.Find(filter).ToList();
                FilterDefinition<RecipeComment>[] filtered0 = new FilterDefinition<RecipeComment>[posts.Count];
                for (int i = 0; i < posts.Count; i++)
                {
                    filtered0[i] = builderPC.Eq(x => x.RecipeId, posts[i].Id);

                }
                var postFilter = builderPC.Or(filtered0);

                var comms = db.RecipeComment.Find(postFilter).ToList();

                FilterDefinition<Comment>[] filteredC = new FilterDefinition<Comment>[comms.Count];
                for (int i = 0; i < comms.Count; i++)
                {
                    if (comms[i].RecipeId == recipeId)
                    {
                        filteredC[i] = builderCom.Eq(x => x.Id, commentId);
                    }
                }

                var filterFin = builderCom.Or(filteredC);

                await db.Comment.DeleteOneAsync(filterFin);
            }
            catch
            {

                throw;
            }
        }
        public async Task UpdateRecipeComment(Comment recipe, string userId, string recipeId, string commentId)
        {
            try
            {
                var builder = Builders<Recipe>.Filter;
                var builderCom = Builders<Comment>.Filter;
                var builderPC = Builders<RecipeComment>.Filter;
                var builderUP = Builders<UserRecipe>.Filter;

                var filterUser = builderUP.Eq(x => x.UserId, userId);
                var users = db.UserRecipe.Find(filterUser).ToList();
                FilterDefinition<Recipe>[] filtered = new FilterDefinition<Recipe>[users.Count];
                for (int i = 0; i < users.Count; i++)
                {
                    if (users[i].UserId == userId)
                    {
                        filtered[i] = builder.Eq(x => x.Id, recipeId);
                    }
                }
                var filter = builder.Or(filtered);

                var recipes = db.Recipe.Find(filter).ToList();
                FilterDefinition<RecipeComment>[] filtered0 = new FilterDefinition<RecipeComment>[recipes.Count];
                for (int i = 0; i < recipes.Count; i++)
                {
                    filtered0[i] = builderPC.Eq(x => x.RecipeId, recipes[i].Id);
                    filtered0[i] = builderPC.Eq(x => x.RecipeId, recipes[i].Id);

                }
                var postFilter = builderPC.Or(filtered0);

                var comms = db.RecipeComment.Find(postFilter).ToList();

                FilterDefinition<Comment>[] filteredC = new FilterDefinition<Comment>[comms.Count];
                for (int i = 0; i < comms.Count; i++)
                {
                    if (comms[i].RecipeId == recipeId)
                    {
                        filteredC[i] = builderCom.Eq(x => x.Id, commentId);
                    }
                }

                var filterFin = builderCom.Or(filteredC);

                await db.Comment.ReplaceOneAsync(filterFin, recipe);
            }
            catch
            {

                throw;
            }
        }

        //--------------------------product---------------------------------------
        public async Task<IEnumerable<Product>> GetProducts(string userId)
        {
            try
            {
                return await db.Product.Find(_ => true).ToListAsync();
            }
            catch
            {
                return null;
            }
            /*try
            {
                var fil = Builders<UserProduct>.Filter.Eq(x => x.UserId, userId);

                var rez = db.UserProduct.Find(fil).ToList();
                if (rez.Count == 0)
                {
                    return null;
                }
                var builder = Builders<Product>.Filter;
                FilterDefinition<Product>[] filtered = new FilterDefinition<Product>[rez.Count];
                for (int i = 0; i < rez.Count; i++)
                {
                    filtered[i] = builder.Eq(u => u.Id, rez[i].ProductId);

                }
                var newFil = builder.Or(filtered);

                return await db.Product.Find(newFil).ToListAsync();
            }
            catch
            {

                return null;
            }*/
        }
        public async Task CreateProduct(Product product, string userId)
        {
            try
            {
                var productCom = new UserProduct();
                await db.Product.InsertOneAsync(product);

                productCom.UserId = userId;
                productCom.ProductId = product.Id;
                await db.UserProduct.InsertOneAsync(productCom);
            }
            catch
            {

                throw;
            }
        }

        public async Task<Product> GetProduct(string userId, string productId)
        {
            try
            {
                FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(s => s.Id, new string(productId));
                return await db.Product.Find(filter).FirstOrDefaultAsync();
            }
            catch
            {

                return null;
            }
            /*try
            {
                var builder = Builders<Product>.Filter;
                var builder0 = Builders<UserProduct>.Filter;
                var filteredProducts = builder0.Eq(x => x.UserId, userId);
                var product = db.UserProduct.Find(filteredProducts).ToList();
                FilterDefinition<Product> filtered = null;
                foreach (var item in product)
                {
                    if (item.ProductId == productId)
                    {
                        filtered = builder.Eq(x => x.Id, productId);
                    }
                }

                return await db.Product.Find(filtered).FirstOrDefaultAsync();
            }
            catch
            {

                return null;
            }*/
        }

        public async Task DeleteProduct(string userId, string productId)
        {


            try
            {
                var builder = Builders<Product>.Filter;
                var builder0 = Builders<UserProduct>.Filter;
                var filteredProduct = builder0.Eq(x => x.UserId, userId);
                var products = db.UserProduct.Find(filteredProduct).ToList();
                FilterDefinition<Product> filtered = null;
                foreach (var item in products)
                {
                    if (item.ProductId == productId)
                    {
                        filtered = builder.Eq(x => x.Id, productId);
                    }
                }
                await db.Product.DeleteOneAsync(filtered);
            }
            catch
            {

                throw;
            }
        }
        public async Task UpdateProduct(Product product, string userId, string productId)
        {
            try
            {
                var builder = Builders<Product>.Filter;
                var builder0 = Builders<UserProduct>.Filter;
                var filteredProduct = builder0.Eq(x => x.UserId, userId);
                var products = db.UserProduct.Find(filteredProduct).ToList();
                FilterDefinition<Product> filtered = null;
                foreach (var item in products)
                {
                    if (item.ProductId == productId)
                    {
                        filtered = builder.Eq(x => x.Id, productId);
                    }
                }

                await db.Product.ReplaceOneAsync(filtered, product);
            }
            catch
            {

                throw;
            }
        }
    }
}
