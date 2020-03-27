using AutoMapper;
using ShoppingTesting.Application.ShoppingCarts.Dtos;
using Microsoft.Extensions.DependencyInjection;

namespace ShoppingTesting.API.Startup
{
    /// <summary>
    /// AutoMapper injection'ları.
    /// </summary>
    public static class ConfigureAutoMapper
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ShoppingCartMapper());
                mc.AddProfile(new CartItemMapper());
                mc.AddProfile(new ProductMapper());
            });
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}