using ShoppingTesting.Application;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ShoppingTesting.API.Startup
{
    /// <summary>
    /// MediatR injection ve configuration'ları.
    /// </summary>
    public static class ConfigureMediatR
    {
        public static void AddMediatR(this IServiceCollection services)
        {
            services.AddMediatR(typeof(RequestValidationException).Assembly)
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionLoggerPipelineBehaviour<,>));
        }
    }
}