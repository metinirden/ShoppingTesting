using System;
using System.Linq;
using ShoppingTesting.Domain;

namespace ShoppingTesting.Infrastructure.Seed
{
    public class DefaultShoppingCartsBuilder
    {
        private readonly ShoppingTestingContext _context;

        public DefaultShoppingCartsBuilder(ShoppingTestingContext context) => _context = context;

        public void Create()
        {
            if (_context.ShoppingCarts.Any())
            {
                return;
            }

            var products = _context.Products.ToList();

            var cartItemsOne = products.Take(2);
            var cartItemsTwo = products.TakeLast(2);

            var shoppingCartOne = new ShoppingCart(Guid.NewGuid());
            var shoppingCartTwo = new ShoppingCart(Guid.NewGuid());

            foreach (var product in cartItemsOne)
            {
                shoppingCartOne.AddItem(new CartItem(Guid.NewGuid(), product, 1), 99);
            }

            foreach (var product in cartItemsTwo)
            {
                shoppingCartTwo.AddItem(new CartItem(Guid.NewGuid(), product, 1), 99);
            }

            _context.ShoppingCarts.AddRange(new[] {shoppingCartOne, shoppingCartTwo});
            _context.SaveChanges();
        }
    }
}