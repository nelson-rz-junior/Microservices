using ProductAPI.Microservice.Entities;
using MongoDB.Driver;
using System.Collections.Generic;

namespace ProductAPI.Microservice.Data
{
    public class AppContextSeed
    {
        public static void SeedData(IMongoCollection<Product> products)
        {
            bool existProduct = products.Find(p => true).Any();
            if (!existProduct)
            {
                products.InsertManyAsync(GetSeedProducts());
            }
        }

        private static IEnumerable<Product> GetSeedProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = "60a85ed57044b3db7d823a06",
                    Category = "Smartphone",
                    Description = "Description 1",
                    Image = "product-1.png",
                    Name = "Product 1",
                    Price = 1000M
                },
                new Product
                {
                    Id = "60a85f0d2f1c8269f1ba30ca",
                    Category = "Smartphone",
                    Description = "Description 2",
                    Image = "product-2.png",
                    Name = "Product 2",
                    Price = 1200M
                },
                new Product
                {
                    Id = "60a85f144ff2afd56cca3341",
                    Category = "Smartphone",
                    Description = "Description 3",
                    Image = "product-3.png",
                    Name = "Product 3",
                    Price = 1400M
                },
                new Product
                {
                    Id = "60a85f144ff2afd56cca4341",
                    Category = "Notebook",
                    Description = "Description 4",
                    Image = "product-4.png",
                    Name = "Product 4",
                    Price = 2400M
                }
            };
        }
    }
}