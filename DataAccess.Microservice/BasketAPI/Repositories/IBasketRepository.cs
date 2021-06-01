using DataAccess.Microservice.BasketAPI.Entities;
using System.Threading.Tasks;

namespace DataAccess.Microservice.BasketAPI.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasket(string userName);

        Task<ShoppingCart> UpdateBasket(ShoppingCart basket);

        Task DeleteBasket(string userName);
    }
}
