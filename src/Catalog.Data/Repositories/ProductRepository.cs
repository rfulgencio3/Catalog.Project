using Catalog.Data.Context;
using Catalog.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _dbContext;
        public ProductRepository(ICatalogContext context)
        {
            _dbContext = context ??
                throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _dbContext.Products.Find(p => true).ToListAsync();
        }
        public async Task<Product> GetProduct(Guid id)
        {
            return await _dbContext.Products.Find(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            FilterDefinition<Product> filterDefinition = Builders<Product>.Filter.Eq(p => p.Category, categoryName);

            return await _dbContext.Products.Find(filterDefinition).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filterDefinition = Builders<Product>.Filter.ElemMatch(p => p.Name, name);

            return await _dbContext.Products.Find(filterDefinition).ToListAsync();
        }
        
        public async Task CreateProduct(Product product)
        {
            await _dbContext.Products.InsertOneAsync(product);
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _dbContext.Products.ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteProduct(Guid id)
        {
            FilterDefinition<Product> filterDefinition = Builders<Product>.Filter.Eq(p => p.Id, id);
            DeleteResult deleteResult = await _dbContext.Products.DeleteOneAsync(filterDefinition);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public void SaveChanges()
        {
            //_dbContext.SaveChanges();
        }

        public void Dispose()
        {
            //_dbContext?.Dispose();
        }
    }
}
