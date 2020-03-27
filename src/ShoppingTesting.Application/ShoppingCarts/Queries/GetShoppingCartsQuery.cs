using System.Collections.Generic;
using System.ComponentModel;
using ShoppingTesting.Application.ShoppingCarts.Dtos;
using MediatR;

namespace ShoppingTesting.Application.ShoppingCarts.Queries
{
    /// <summary>
    /// Sistemdeki bütün sepetleri dönecek olan Query object'i.
    /// </summary>
    public class GetShoppingCartsQuery : IRequest<List<ShoppingCartDto>>
    {
        [DefaultValue(false)] public bool IncludeCartItems { get; set; } = false;
    }
}