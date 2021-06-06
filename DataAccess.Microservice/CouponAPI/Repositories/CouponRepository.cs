using Dapper;
using DataAccess.Microservice.CouponAPI.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Threading.Tasks;

namespace DataAccess.Microservice.CouponAPI.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private readonly IConfiguration _configuration;

        public CouponRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentException(nameof(configuration));
        }

        public async Task<Coupon> GetCoupon(string productName)
        {
            using var connection = GetPostgreSQLConnection();

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>("SELECT * FROM Coupon WHERE ProductName = @ProductName", new
                {
                    ProductName = productName
                });

            return coupon ?? new Coupon
                {
                    ProductName = "No Coupon",
                    Amount = 0M,
                    Description = "No Coupon Description"
                };
        }

        public async Task<bool> CreateCoupon(Coupon coupon)
        {
            using var connection = GetPostgreSQLConnection();

            var rowsAffected = await connection.ExecuteAsync("INSERT INTO Coupon (ProductName, Description, Amount) " +
                "VALUES (@ProductName, @Description, @Amount)", new
                {
                    ProductName = coupon.ProductName,
                    Description = coupon.Description,
                    Amount = coupon.Amount
                });

            return rowsAffected != 0;
        }

        public async Task<bool> UpdateCoupon(Coupon coupon)
        {
            using var connection = GetPostgreSQLConnection();

            var rowsAffected = await connection.ExecuteAsync("UPDATE Coupon SET ProductName = @ProductName, Description = @Description, Amount = @Amount " +
                "WHERE Id = @Id", new
                {
                    ProductName = coupon.ProductName,
                    Description = coupon.Description,
                    Amount = coupon.Amount,
                    Id = coupon.Id
                });

            return rowsAffected != 0;
        }

        public async Task<bool> DeleteCoupon(string productName)
        {
            using var connection = GetPostgreSQLConnection();

            var rowsAffected = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName = @ProductName", new
                {
                    ProductName = productName
                });

            return rowsAffected != 0;
        }

        private NpgsqlConnection GetPostgreSQLConnection()
        {
            return new NpgsqlConnection(_configuration["DatabaseSettings:ConnectionString"]);
        }
    }
}
