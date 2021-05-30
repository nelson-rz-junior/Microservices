using System.Collections.Generic;
using System.Linq;

namespace BasketAPI.Microservice.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; }

        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

        public ShoppingCart()
        {
        }

        public ShoppingCart(string userName)
        {
            UserName = userName;
        }

        public decimal TotalPrice 
        {
            get
            {
                return Items.Sum(i => i.Quantity * i.Price);
            }
        }
    }
}
