using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ShoppingTesting.API.Startup
{
    /// <summary>
    /// Swagger injection'ları.
    /// </summary>
    public static class ConfigureSwagger
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options => options.SwaggerDoc("V1", new OpenApiInfo
            {
                Title = "ShoppingTesting API", Version = "V1"
            }));
        }
    }
}