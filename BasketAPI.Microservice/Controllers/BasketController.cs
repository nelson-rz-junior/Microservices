using BasketAPI.Microservice.Services;
using DataAccess.Microservice.BasketAPI.Entities;
using DataAccess.Microservice.BasketAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BasketAPI.Microservice.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;

        private readonly ICouponGrpc _coupon;

        public BasketController(IBasketRepository repository, ICouponGrpc coupon)
        {
            _repository = repository ?? throw new ArgumentException(nameof(repository));
            _coupon = coupon ?? throw new ArgumentException(nameof(coupon));
        }

        [HttpGet("{userName}")]
        [ProducesResponseType(typeof(ShoppingCart), StatusCodes.Status200OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var basket = await _repository.GetBasket(userName);

            return Ok(basket ?? new ShoppingCart(userName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), StatusCodes.Status200OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket(ShoppingCart basket)
        {
            if (basket is null)
            {
                return BadRequest("Invalid basket");
            }

            foreach (var item in basket.Items)
            {
                var coupon = await _coupon.GetCoupon(item.ProductName);
                item.Price -= (decimal)coupon.Amount;
            }

            return Ok(await _repository.UpdateBasket(basket));
        }

        [HttpDelete("{userName}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            await _repository.DeleteBasket(userName);

            return NoContent();
        }
    }
}
