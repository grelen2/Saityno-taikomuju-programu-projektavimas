using RestGreta.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace RestGreta.Data.Repositories
{
    public interface IProductsRepository
    {
        
        Task<Product> GetProduct(string id);
        Task<IEnumerable<Product>> GetAllProducts();
        Task PutProduct(Product product);
        Task CreateProduct(Product product);
        Task DeleteProduct(string id);
    }

    public class ProductsRepository : IProductsRepository
    {
        internal MongoDBContext db = new MongoDBContext();
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            try
            {
                return await db.Product.Find(_ => true).ToListAsync();
            }
            catch
            {
                return null;
            }
        }
        public async Task<Product> GetProduct(string id)
        {
            try
            {
                FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(s =>  s.Id, new string(id));
                return await db.Product.Find(filter).FirstOrDefaultAsync();
            }
            catch
            {

                return null;
            }

        }
        public async Task CreateProduct(Product product)
        {
            try
            {
                await db.Product.InsertOneAsync(product);

            }
            catch
            {

                throw;
            }
        }
        public async Task PutProduct(Product product)
        {
            try
            {
                var filter = Builders<Product>
                    .Filter
                    .Eq(s => s.Id, product.Id);
                await db.Product.ReplaceOneAsync(filter, product);
            }
            catch
            {

                throw;
            }
        }
        public async Task DeleteProduct(string id)
        {

            try
            {
                var filter = Builders<Product>.Filter.Eq(s => s.Id, new string(id));
                await db.Product.DeleteOneAsync(filter);
            }
            catch
            {

                throw;
            }
        }
    }
}
