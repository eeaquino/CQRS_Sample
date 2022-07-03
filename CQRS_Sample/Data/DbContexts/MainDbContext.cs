using CQRS_Sample.Data.Entities.Customers;
using Microsoft.EntityFrameworkCore;
namespace CQRS_Sample.Data.DbContexts
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasKey(k => k.CustomerId);
            modelBuilder.Entity<Customer>().ToTable("customers");
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
