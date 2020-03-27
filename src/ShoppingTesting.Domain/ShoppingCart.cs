using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingTesting.Domain.Exception;
using ShoppingTesting.Domain.SharedKernel;

namespace ShoppingTesting.Domain
{
    /// <summary>
    /// ShoppingCart domain object'si, sepetin oluşturulması ve ürün eklenmesi gibi logic'leri içinde barındırır.
    /// </summary>
    public class ShoppingCart : Entity<Guid>
    {
        public List<CartItem> CartItems { get; private set; } = new List<CartItem>();

        private ShoppingCart()
        {
        }

        public ShoppingCart(Guid id) : base(id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException($"Id cannot be default/empty.");
            }
        }

        public void AddItem(CartItem cartItem, int currentStock)
        {
            if (cartItem is null)
            {
                throw new DomainException($"{nameof(cartItem)} cannot be null!", new InvalidOperationException());
            }

            var existingCartItem = CartItems.SingleOrDefault(
                p => p.Product.Equals(cartItem.Product)
            );

            ValidateStock(existingCartItem, cartItem, currentStock);

            if (existingCartItem is null)
            {
                CartItems.Add(cartItem);
            }
            else
            {
                existingCartItem.Increase(cartItem.Quantity);
            }
        }

        private void ValidateStock(CartItem existingCartItem, CartItem newCartItem, int currentStock)
        {
            if (currentStock <= 0)
            {
                throw new DomainException("Insufficient stock!", new InvalidOperationException());
            }

            uint targetQuantity = (existingCartItem?.Quantity ?? 0) + newCartItem.Quantity;
            if (targetQuantity > currentStock)
            {
                throw new DomainException("Quantity cannot add greater than stock!", new InvalidOperationException());
            }
        }
    }
}