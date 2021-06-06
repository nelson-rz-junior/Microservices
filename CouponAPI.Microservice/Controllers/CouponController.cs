using DataAccess.Microservice.CouponAPI.Entities;
using DataAccess.Microservice.CouponAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CouponAPI.Microservice.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICouponRepository _repository;

        public CouponController(ICouponRepository repository)
        {
            _repository = repository ?? throw new ArgumentException(nameof(repository));
        }

        [HttpGet("{productName}", Name = "GetCoupon")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Coupon), StatusCodes.Status200OK)]
        public async Task<ActionResult<Coupon>> GetCoupon(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
            {
                return BadRequest("Invalid product name");
            }

            var coupon = await _repository.GetCoupon(productName);

            return Ok(coupon);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Coupon), StatusCodes.Status201Created)]
        public async Task<ActionResult<Coupon>> CreateCoupon(Coupon coupon)
        {
            if (coupon is null)
            {
                return BadRequest("Invalid coupon");
            }

            await _repository.CreateCoupon(coupon);

            return CreatedAtRoute("GetCoupon", new { productName = coupon.ProductName }, coupon);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateCoupon(Coupon coupon)
        {
            if (coupon is null)
            {
                return BadRequest("Invalid coupon");
            }

            var result = await _repository.UpdateCoupon(coupon);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{productName}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCoupon(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
            {
                return BadRequest("Invalid product name");
            }

            bool result = await _repository.DeleteCoupon(productName);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
