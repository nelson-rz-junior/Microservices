using CouponAPI.Grpc;
using System.Threading.Tasks;

namespace BasketAPI.Microservice.Services
{
    public interface ICouponGrpc
    {
        Task<CouponModel> GetCoupon(string productName);
    }
}
