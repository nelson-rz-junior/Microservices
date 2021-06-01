using DataAccess.Microservice.ProductAPI.Entities;
using MongoDB.Driver;

namespace DataAccess.Microservice.ProductAPI.Context
{
    public interface IAppDbContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
