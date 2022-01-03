using Dapper;
using Discount.API.Entities;
using Npgsql;

namespace Discount.API.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;
        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        private NpgsqlConnection GetConnectionPostgreSQL()
        {
            return new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            NpgsqlConnection connection = GetConnectionPostgreSQL();

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                ("SELECT * FROM Coupon WHERE ProductName = @ProductName",
                new { ProductName = productName });

            if (coupon == null)
                return new Coupon
                { ProductName = "NO_DISCOUNT", Amount = 0, Description = "NO_DISCOUNT_DESC" };

            return coupon;
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            NpgsqlConnection connection = GetConnectionPostgreSQL();

            var newCoupon = await connection.ExecuteAsync
                ("INSERT INTO Coupon (ProductName, Description, Amount)" + 
                " VALUES (@ProductName, @Description, @Amount)",
                new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });

            if (newCoupon == 0) return false;
            return true;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            NpgsqlConnection connection = GetConnectionPostgreSQL();

            var updateCoupon = await connection.ExecuteAsync
                ("UPDATE Coupon SET ProductName = @ProductName, Description = @Description, Amount = @Amount" +
                " WHERE Id = @Id",
                new
                {
                    ProductName = coupon.ProductName,
                    Description = coupon.Description,
                    Amount = coupon.Amount,
                    Id = coupon.Id
                });

            if (updateCoupon == 0) return false;
            return true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            NpgsqlConnection connection = GetConnectionPostgreSQL();

            var deleteCoupon = await connection.ExecuteAsync
                ("DELETE FROM Coupon WHERE ProductName = @ProductName",
                new { ProductName = productName });

            if (deleteCoupon == 0) return false;
            return true;
        }
    }
}
