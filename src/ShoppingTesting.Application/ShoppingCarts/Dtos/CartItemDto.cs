using AutoMapper;
using ShoppingTesting.Domain;

namespace ShoppingTesting.Application.ShoppingCarts.Dtos
{
    /// <summary>
    /// CartItem domain object'sinin DTO'su
    /// </summary>
    public class CartItemDto
    {
        public ProductDto Product { get; set; }
        public uint Quantity { get; set; }
    }

    public class CartItemMapper : Profile
    {
        public CartItemMapper()
        {
            CreateMap<CartItem, CartItemDto>();
        }
    }
}