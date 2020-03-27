using System;
using System.Threading.Tasks;

namespace ShoppingTesting.Domain
{
    /// <summary>
    /// External ürün stok abstraction'ı.
    /// </summary>
    public interface IProductStockProvider
    {
        Task<int> GetStockForProduct(Guid productId);
    }
}