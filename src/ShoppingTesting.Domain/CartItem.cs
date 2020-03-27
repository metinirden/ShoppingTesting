using System;
using ShoppingTesting.Domain.Exception;
using ShoppingTesting.Domain.SharedKernel;

namespace ShoppingTesting.Domain
{
    public class CartItem : Entity<Guid>
    {
        public ShoppingCart ShoppingCart { get; set; }
        public Product Product { get; private set; }
        public uint Quantity { get; private set; }

        private CartItem()
        {
        }

        public CartItem(Guid id, Product product, uint quantity) : base(id)
        {
            Product = product ?? throw new DomainException($"{nameof(product)} cannot be nul!",
                          new ArgumentNullException(nameof(product)));
            Quantity = quantity;
        }

        internal void Increase(uint count)
        {
            Quantity += count;
        }
    }
}