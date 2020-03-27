using System;
using ShoppingTesting.Domain.Exception;
using ShoppingTesting.Domain.SharedKernel;

namespace ShoppingTesting.Domain
{
    /// <summary>
    /// Product domain object'si, ürünün oluşturulması ve fiyatının değiştirilmesi gibi logic'leri içinde barındırır.
    /// </summary>
    public class Product : Entity<Guid>
    {
        public string Name { get; private set; }
        public Price Price { get; private set; }

        private Product()
        {
        }

        public Product(Guid id, string name, Price price) : base(id)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainException($"{nameof(name)} cannot be null/empty/whitespace on Product",
                    new ArgumentException(string.Empty, nameof(name)));
            }

            Name = name;

            Price = price ?? throw new DomainException($"{nameof(price)} cannot be null!",
                        new ArgumentNullException(nameof(price)));
        }

        public void ChangePrice(Price newPrice)
        {
            Price = newPrice ?? throw new ArgumentNullException(nameof(newPrice));
        }
    }
}