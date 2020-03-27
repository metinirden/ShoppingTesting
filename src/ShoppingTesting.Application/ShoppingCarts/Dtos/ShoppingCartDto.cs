using System;
using System.Collections.Generic;
using AutoMapper;
using ShoppingTesting.Domain;

namespace ShoppingTesting.Application.ShoppingCarts.Dtos
{
    /// <summary>
    /// ShoppingCart domain object'sinin DTO'su
    /// </summary>
    public class ShoppingCartDto
    {
        public Guid Id { get; set; }
        public List<CartItemDto> CartItems { get; set; }
    }

    public class ShoppingCartMapper : Profile
    {
        public ShoppingCartMapper()
        {
            CreateMap<ShoppingCart, ShoppingCartDto>();
        }
    }

}