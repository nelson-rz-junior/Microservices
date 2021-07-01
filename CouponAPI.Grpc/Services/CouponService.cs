using AutoMapper;
using DataAccess.Microservice.CouponAPI.Repositories;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using entities = DataAccess.Microservice.CouponAPI.Entities;

namespace CouponAPI.Grpc.Services
{
    public class CouponService : Coupon.CouponBase
    {
        private readonly ILogger<CouponService> _logger;

        private readonly IMapper _mapper;

        private readonly ICouponRepository _repository;

        public CouponService(ICouponRepository repository, ILogger<CouponService> logger, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            _repository = repository ?? throw new ArgumentException(nameof(repository));
        }

        public override async Task<CouponModel> GetCoupon(CouponGetRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Begin gRPC call from method {Method}", context.Method);

            var coupon = await _repository.GetCoupon(request.ProductName);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Coupon for {request.ProductName} not found."));
            }

            var couponModel = _mapper.Map<CouponModel>(coupon);

            context.Status = new Status(StatusCode.OK, $"Coupon created for {request.ProductName}");

            return couponModel;
        }

        public override async Task<CouponModel> CreateCoupon(CouponCreateRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Begin gRPC call from method {Method}", context.Method);

            var coupon = _mapper.Map<entities.Coupon>(request.Coupon);

            await _repository.CreateCoupon(coupon);

            var couponModel = _mapper.Map<CouponModel>(coupon);

            context.Status = new Status(StatusCode.OK, $"Coupon for {request.Coupon.ProductName} created successfully");

            return couponModel;
        }

        public override async Task<Empty> UpdateCoupon(CouponUpdateRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Begin gRPC call from method {Method}", context.Method);

            var coupon = _mapper.Map<entities.Coupon>(request.Coupon);

            await _repository.UpdateCoupon(coupon);

            context.Status = new Status(StatusCode.OK, $"Coupon for {request.Coupon.ProductName} updated successfully");

            return new Empty();
        }

        public override async Task<CouponDeleteResponse> DeleteCoupon(CouponDeleteRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Begin gRPC call from method {Method}", context.Method);

            var result = new CouponDeleteResponse
            {
                Success = await _repository.DeleteCoupon(request.ProductName)
            };

            if (result.Success)
            {
                context.Status = new Status(StatusCode.OK, $"Coupon for {request.ProductName} deleted successfully");
            }
            else
            {
                context.Status = new Status(StatusCode.NotFound, $"Coupon for {request.ProductName} not found");
            }

            return result;
        }
    }
}
