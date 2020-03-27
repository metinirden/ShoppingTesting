using System;
using System.Threading.Tasks;
using ShoppingTesting.Domain;

namespace ShoppingTesting.Infrastructure
{
    public class ProductStockProvider : IProductStockProvider
    {
        public Task<int> GetStockForProduct(Guid productId)
        {
            var randomStock = 50;
            return Task.FromResult(randomStock);
        }
    }
}