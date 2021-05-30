using ProductAPI.Microservice.Entities;
using MongoDB.Driver;

namespace ProductAPI.Microservice.Data
{
    public interface IAppDbContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
