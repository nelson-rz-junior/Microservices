using ProductAPI.Microservice.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace ProductAPI.Microservice.Data
{
    public class AppDbContext : IAppDbContext
    {
        public AppDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);

            Products = database.GetCollection<Product>(configuration["DatabaseSettings:CollectionName"]);

            AppContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
