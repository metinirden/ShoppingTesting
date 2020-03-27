using ShoppingTesting.Domain;
using ShoppingTesting.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace ShoppingTesting.API.Startup
{
    /// <summary>
    /// EFCore injection'ları.
    /// </summary>
    public static class ConfigureEntityFrameworkCore
    {
        public static void AddEfCore(this IServiceCollection services)
        {
            services.AddDbContext<ShoppingTestingContext>();
            services.AddTransient(typeof(IRepository<,>), typeof(EfRepository<,>));
            services.AddTransient<IProductStockProvider, ProductStockProvider>();
        }
    }
}