using ShoppingTesting.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShoppingTesting.Infrastructure.Configurations
{
    public class ShoppingCartTypeConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.HasKey(k => k.Id);
            builder.HasMany(p => p.CartItems).WithOne(p => p.ShoppingCart);
        }
    }
}