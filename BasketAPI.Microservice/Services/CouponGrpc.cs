using CouponAPI.Grpc;
using System.Threading.Tasks;

namespace BasketAPI.Microservice.Services
{
    public class CouponGrpc : ICouponGrpc
    {
        private readonly Coupon.CouponClient _coupon;

        public CouponGrpc(Coupon.CouponClient coupon)
        {
            _coupon = coupon;
        }

        public async Task<CouponModel> GetCoupon(string productName)
        {
            var request = new CouponGetRequest
            {
                ProductName = productName
            };

            return await _coupon.GetCouponAsync(request);
        }
    }
}
