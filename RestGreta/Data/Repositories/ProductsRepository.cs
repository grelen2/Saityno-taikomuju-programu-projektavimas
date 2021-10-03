using RestGreta.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestGreta.Data.Repositories
{
    public interface IProductsRepository
    {
        
        Task<Product> Get(int id);
        Task<IEnumerable<Product>> GetAll();
        Task<Product> Put(Product product);
        Task<Product> Create(Product product);//(Product product);
        Task Delete(Product product);
    }

    public class ProductsRepository : IProductsRepository
    {
        public async Task<IEnumerable<Product>> GetAll()
        {
            return new List<Product>
            {
                new Product()
                {
                    Name = "Ryžiai",
                    CreationTimeUtc = DateTime.UtcNow
                },
                new Product()
                {
                    Name = "Obuolys",
                    CreationTimeUtc = DateTime.UtcNow
                }
            };
        }
        public async Task<Product> Get(int id)
        {
            return new Product()
            {
                Name = "Ryžiai",
                CreationTimeUtc = DateTime.UtcNow
            };

        }
        public async Task<Product> Create(Product product)
        {
            return new Product()
            {
                Name = "Kruopos",
                CreationTimeUtc = DateTime.UtcNow
            };
        }
        public async Task<Product> Put(Product product)
        {
            return new Product()
            {
                Name = "Duona",
                CreationTimeUtc = DateTime.UtcNow
            };
        }
        public async Task Delete(Product product)
        {
        }
    }
}
