using Grpc.Net.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BasketAPI.Grpc.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var httpHandler = new HttpClientHandler();

            // Return true to allow certificates that are untrusted/invalid
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("https://dockerhost:5001", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new Basket.BasketClient(channel);

            var reply = await client.GetBasketAsync(new BasketGetRequest
            {
                UserName = "user01"
            });

            foreach (var item in reply.Items)
            {
                Console.WriteLine($"ProductId: {item.ProductId}");
                Console.WriteLine($"Product Name: {item.ProductName}");
                Console.WriteLine($"Quantity: {item.Quantity}");
                Console.WriteLine($"Price: {item.Price:c}");
            }

            Console.WriteLine("Press any key to exit...");

            Console.ReadKey();
        }
    }
}
