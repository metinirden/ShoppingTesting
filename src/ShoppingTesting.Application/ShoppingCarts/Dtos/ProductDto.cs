using System;
using AutoMapper;
using ShoppingTesting.Domain;

namespace ShoppingTesting.Application.ShoppingCarts.Dtos
{
    /// <summary>
    /// Product domain object'sinin DTO'su
    /// </summary>
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Currency { get; set; }
        public double Amount { get; set; }
    }

    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(m => m.Currency, s => s.MapFrom(product => product.Price.Currency))
                .ForMember(m => m.Amount, s => s.MapFrom(product => product.Price.Amount));
        }
    }
}