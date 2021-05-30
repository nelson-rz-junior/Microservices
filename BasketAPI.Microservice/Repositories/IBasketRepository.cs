using BasketAPI.Microservice.Entities;
using System.Threading.Tasks;

namespace BasketAPI.Microservice.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasket(string userName);

        Task<ShoppingCart> UpdateBasket(ShoppingCart basket);

        Task DeleteBasket(string userName);
    }
}
