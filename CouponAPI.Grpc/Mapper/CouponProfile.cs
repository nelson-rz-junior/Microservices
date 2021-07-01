using AutoMapper;
using entities = DataAccess.Microservice.CouponAPI.Entities;

namespace CouponAPI.Grpc.Mapper
{
    public class CouponProfile : Profile
    {
        public CouponProfile()
        {
            CreateMap<entities.Coupon, CouponModel>().ReverseMap();
        }
    }
}
