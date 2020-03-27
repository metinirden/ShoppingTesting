using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingTesting.Domain;

namespace ShoppingTesting.Infrastructure.Seed
{
    internal class DefaultProductsBuilder
    {
        private readonly ShoppingTestingContext _context;

        public DefaultProductsBuilder(ShoppingTestingContext context) => _context = context;

        public void Create()
        {
            if (_context.Products.Any())
            {
                return;
            }


            var defaultProducts = new List<Product>
            {
                new Product(Guid.NewGuid(), "Gerbera ve Beyaz Gül Aranjmanı", new Price("TRY", 54.99)),
                new Product(Guid.NewGuid(), "2 Dal Beyaz Orkide Çiçeği", new Price("TRY", 94.99)),
                new Product(Guid.NewGuid(), "Topaz Melek Kanadı 0,02 Crt. Pırlanta Gümüş Kolye Cc84", new Price("TRY", 166.99)),
                new Product(Guid.NewGuid(), "Premium Karışık Çikolata Kutusu 310 gr", new Price("TRY", 59.99))
            };

            _context.Products.AddRange(defaultProducts);
            _context.SaveChanges();
        }
    }
}