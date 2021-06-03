using DataAccess.Microservice.BasketAPI.Entities;
using DataAccess.Microservice.BasketAPI.Repositories;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketAPI.Grpc
{
    [AllowAnonymous]
    public class BasketService : Basket.BasketBase
    {
        private readonly ILogger<BasketService> _logger;

        private readonly IBasketRepository _repository;

        public BasketService(IBasketRepository repository, ILogger<BasketService> logger)
        {
            _logger = logger;
            _repository = repository ?? throw new ArgumentException(nameof(repository));
        }

        public override async Task<BasketResponse> GetBasket(BasketGetRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Begin gRPC call from method {Method} for {UserName}", context.Method, request.UserName);

            var response = await _repository.GetBasket(request.UserName);
            if (response != null)
            {
                context.Status = new Status(StatusCode.OK, $"Basket for {request.UserName} do exist");
                return MapToBasketResponse(response);
            }
            else
            {
                context.Status = new Status(StatusCode.OK, $"Basket created for {request.UserName}");
                return MapToBasketResponse(new ShoppingCart
                {
                    UserName = request.UserName,
                    Items = new List<ShoppingCartItem>()
                });
            }
        }

        public override async Task<BasketResponse> UpdateBasket(BasketUpdateRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Begin gRPC call from method {Method} for {UserName}", context.Method, request.UserName);

            var basket = MapToShoppingCart(request);

            var response = await _repository.UpdateBasket(basket);
            if (response != null)
            {
                context.Status = new Status(StatusCode.OK, $"Basket for {request.UserName} updated successfully");
                return MapToBasketResponse(response);
            }

            context.Status = new Status(StatusCode.NotFound, $"Basket for {request.UserName} do not exist");

            return new BasketResponse();
        }

        public override async Task<Empty> DeleteBasket(BasketDeleteRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Begin gRPC call from method {Method} for {UserName}", context.Method, request.UserName);

            await _repository.DeleteBasket(request.UserName);

            context.Status = new Status(StatusCode.OK, $"Basket for {request.UserName} deleted succesfully");

            return new Empty();
        }

        private BasketResponse MapToBasketResponse(ShoppingCart basket)
        {
            var response = new BasketResponse
            {
                UserName = basket.UserName
            };

            basket.Items.ForEach(item => response.Items.Add(new BasketItemResponse
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                Price = (double)item.Price
            }));

            return response;
        }

        private ShoppingCart MapToShoppingCart(BasketUpdateRequest request)
        {
            var response = new ShoppingCart
            {
                UserName = request.UserName
            };

            request.Items.ToList().ForEach(item => response.Items.Add(new ShoppingCartItem
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                Price = (decimal)item.Price
            }));

            return response;
        }
    }
}
