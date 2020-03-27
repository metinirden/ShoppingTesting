using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingTesting.Domain;
using ShoppingTesting.Domain.Exception;
using Xunit;

namespace ShoppingTesting.Test
{
    /// <summary>
    /// ShoppingCart construction ve AddItem test'leri.
    /// </summary>
    public class ShoppingCartTests
    {
        [Fact]
        public void Construct_ShouldThrowArgumentException_WithEmptyGuidAndEmptyCartItems()
        {
            List<CartItem> cartItems = new List<CartItem>();
            Assert.Throws<ArgumentException>(() => { new ShoppingCart(Guid.Empty); });
        }

        [Fact]
        public void AddItem_ShouldThrowArgumentNullException_WithNullCartItem()
        {
            var shoppingCart = new ShoppingCart(Guid.NewGuid());
            Assert.Throws<ArgumentNullException>(() => { shoppingCart.AddItem(null, 1); });
        }

        [Theory, InlineData(new object[] {0})]
        public void AddItem_ShouldThrowDomainException_WithPropertCartItemButWarehouseStockBelowOne(int currentStock)
        {
            var shoppingCart = new ShoppingCart(Guid.NewGuid());
            var product = new Product(Guid.NewGuid(), "Test", new Price("TRY", 55.55));
            var cartItem = new CartItem(Guid.NewGuid(), product, 1);

            Assert.Throws<DomainException>(() => { shoppingCart.AddItem(cartItem, currentStock); });
        }

        [Fact]
        public void AddItem_ShouldThrowDomainException_WhenCartItemStockGreaterThanWarehouseStock()
        {
            var shoppingCart = new ShoppingCart(Guid.NewGuid());
            var product = new Product(Guid.NewGuid(), "Test", new Price("TRY", 55.55));
            var cartItem = new CartItem(Guid.NewGuid(), product, 5);

            Assert.Throws<DomainException>(() => { shoppingCart.AddItem(cartItem, 1); });
        }

        [Fact]
        public void AddItem_ShouldReturnsOneCartItem_WhenAddingToNewShoppingCart()
        {
            var shoppingCart = new ShoppingCart(Guid.NewGuid());
            var product = new Product(Guid.NewGuid(), "Test", new Price("TRY", 55.55));
            var cartItem = new CartItem(Guid.NewGuid(), product, 5);
            shoppingCart.AddItem(cartItem, 10);

            Assert.Single(shoppingCart.CartItems);
        }

        [Fact]
        public void AddItem_ShouldReturnsOneCartItem_WhenAddingAlreadyExistingCartItem()
        {
            var shoppingCart = new ShoppingCart(Guid.NewGuid());
            var product = new Product(Guid.NewGuid(), "Test", new Price("TRY", 55.55));
            var cartItem = new CartItem(Guid.NewGuid(), product, 5);

            shoppingCart.AddItem(cartItem, 10);
            shoppingCart.AddItem(cartItem, 10);

            Assert.Single(shoppingCart.CartItems);
        }

        [Fact]
        public void AddItem_ShouldReturnsCorrectQuantity_WhenAddingAlreadyExistingCartItem()
        {
            var shoppingCart = new ShoppingCart(Guid.NewGuid());
            var product = new Product(Guid.NewGuid(), "Test", new Price("TRY", 55.55));
            var cartItem = new CartItem(Guid.NewGuid(), product, 5);
            shoppingCart.AddItem(cartItem, 10);
            shoppingCart.AddItem(cartItem, 10);

            Assert.Equal((uint) 10, shoppingCart.CartItems.First().Quantity);
        }

        [Fact]
        public void AddItem_ShouldReturnsOneCartItem_WhenAddingDifferentCartItem()
        {
            var shoppingCart = new ShoppingCart(Guid.NewGuid());
            var product1 = new Product(Guid.NewGuid(), "Test", new Price("TRY", 55.55));
            var cartItem = new CartItem(Guid.NewGuid(), product1, 5);
            var product2 = new Product(Guid.NewGuid(), "Test2 ", new Price("TRY", 55.55));
            var cartItem2 = new CartItem(Guid.NewGuid(), product2, 5);
            shoppingCart.AddItem(cartItem, 10);
            shoppingCart.AddItem(cartItem2, 5);

            Assert.Equal(2, shoppingCart.CartItems.Count);
        }
    }
}