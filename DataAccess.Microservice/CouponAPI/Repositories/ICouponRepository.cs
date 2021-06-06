using DataAccess.Microservice.CouponAPI.Entities;
using System.Threading.Tasks;

namespace DataAccess.Microservice.CouponAPI.Repositories
{
    public interface ICouponRepository
    {
        Task<Coupon> GetCoupon(string productName);

        Task<bool> CreateCoupon(Coupon coupon);

        Task<bool> UpdateCoupon(Coupon coupon);

        Task<bool> DeleteCoupon(string productName);
    }
}
