using ShoppingTesting.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShoppingTesting.Infrastructure.Configurations
{
    public class CartItemTypeConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Quantity).IsRequired();
            builder.HasOne(p => p.ShoppingCart).WithMany(p => p.CartItems);
        }
    }
}