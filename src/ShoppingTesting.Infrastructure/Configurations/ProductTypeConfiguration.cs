using ShoppingTesting.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace ShoppingTesting.Infrastructure.Configurations
{
    /// <summary>
    /// Ek olarak Price ValueObject'i kolona serialize edilerek yazılıp, deserialize edilerek okunuyor.
    /// </summary>
    public class ProductTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);

            var converter = new ValueConverter<Price, string>
            (
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<Price>(v)
            );

            var comparer = new ValueComparer<Price>
            (
                (l, r) => JsonConvert.SerializeObject(l) == JsonConvert.SerializeObject(r),
                v => v == null ? 0 : JsonConvert.SerializeObject(v).GetHashCode(),
                v => JsonConvert.DeserializeObject<Price>(JsonConvert.SerializeObject(v))
            );

            builder.Property(p => p.Price).HasConversion(converter);
            builder.Property(p => p.Price).Metadata.SetValueConverter(converter);
            builder.Property(p => p.Price).Metadata.SetValueComparer(comparer);
        }
    }
}