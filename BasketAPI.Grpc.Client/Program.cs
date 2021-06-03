using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BasketAPI.Grpc.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var httpHandler = new HttpClientHandler
            {
                // Return true to allow certificates that are untrusted/invalid
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("https://dockerhost:5001", new GrpcChannelOptions
            {
                HttpHandler = httpHandler
            });

            var client = new Basket.BasketClient(channel);

            var reply = await client.GetBasketAsync(new BasketGetRequest
            {
                UserName = Guid.NewGuid().ToString()
            });

            foreach (var item in reply.Items)
            {
                Console.WriteLine($"ProductId: {item.ProductId}");
                Console.WriteLine($"Product Name: {item.ProductName}");
                Console.WriteLine($"Quantity: {item.Quantity}");
                Console.WriteLine($"Price: {item.Price:c}");
            }

            Console.WriteLine($"Basket created for {reply.UserName}");

            Console.WriteLine();

            var basketUpdate = new BasketUpdateRequest
            {
                UserName = reply.UserName
            };

            basketUpdate.Items.AddRange(new List<BasketItemResponse>
            {
                new BasketItemResponse { ProductId = "123", ProductName = "Product 01", Quantity = 2, Price = 10 },
                new BasketItemResponse { ProductId = "124", ProductName = "Product 02", Quantity = 3, Price = 20 },
                new BasketItemResponse { ProductId = "125", ProductName = "Product 03", Quantity = 4, Price = 30 },
                new BasketItemResponse { ProductId = "126", ProductName = "Product 04", Quantity = 1, Price = 40 },
                new BasketItemResponse { ProductId = "127", ProductName = "Product 05", Quantity = 2, Price = 50 }
            });

            var response = await client.UpdateBasketAsync(basketUpdate);

            Console.WriteLine($"Basket updated for {reply.UserName}");

            Console.WriteLine();

            foreach (var item in response.Items)
            {
                Console.WriteLine($"ProductId: {item.ProductId}");
                Console.WriteLine($"Product Name: {item.ProductName}");
                Console.WriteLine($"Quantity: {item.Quantity}");
                Console.WriteLine($"Price: {item.Price:c}");

                Console.WriteLine("------------------------------------");
            }

            Console.WriteLine("Press any key to exit...");

            Console.ReadKey();
        }
    }
}
