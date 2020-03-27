using System.Collections.Generic;
using ShoppingTesting.Application.ShoppingCarts.Dtos;
using MediatR;

namespace ShoppingTesting.Application.ShoppingCarts.Queries
{
    /// <summary>
    /// Sistemdeki ürünleri dönecek Query object'i.
    /// </summary>
    public class GetProductsQuery : IRequest<List<ProductDto>>
    {
    }
}