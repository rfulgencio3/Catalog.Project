using Catalog.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace Catalog.Data.Context
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetMyProducts());
            }
        }

        private static IEnumerable<Product> GetMyProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "IPhone X",
                    Category = "Smartphone",
                    Description = "personal smartphone",
                    Image = "iphone-x-202112.png",
                    Price = 2989.00
                }
            };
        }
    }
}