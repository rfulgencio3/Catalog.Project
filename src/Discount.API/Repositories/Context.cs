using Discount.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Discount.API.Repositories
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);

            modelBuilder.Entity<Coupon>(e => { e.HasNoKey(); });
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}