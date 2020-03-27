using ShoppingTesting.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ShoppingTesting.Infrastructure
{
    public class ShoppingTestingContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public ShoppingTestingContext(DbContextOptions<ShoppingTestingContext> options, IConfiguration configuration) :
            base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //ConnectionString ve TransientFailure için Retry configuration'ı.
            optionsBuilder.UseMySql(_configuration.GetConnectionString("Default"),
                builder => { builder.EnableRetryOnFailure(5); });
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //ValueObject olan Price db'ye bir table olarak yansıtılmayacaktır.
            modelBuilder.Ignore<Price>();
            
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}