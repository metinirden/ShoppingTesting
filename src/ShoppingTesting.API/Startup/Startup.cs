using System.Threading.Tasks;
using ShoppingTesting.Infrastructure;
using ShoppingTesting.Infrastructure.Seed;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ShoppingTesting.API.Startup
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEfCore();
            services.AddAutoMapper();
            services.AddMediatR();
            services.AddProblemDetails();
            services.AddSwagger();

            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                //ValidationPipelineBehaviour ilgilendiği için, otomatik model validation'ı kapatıldı.
                options.SuppressModelStateInvalidFilter = true;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ShoppingTestingContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (env.IsProduction())
            {
                //Environment production ise, otomatik migration vs seed lazımsa seeding.
                dbContext.Database.Migrate();
                dbContext.SeedHostDb();
            }

            app.UseProblemDetails();
            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("V1/swagger.json", "ShoppingTesting API V1"));
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.Map("/", context =>
                {
                    context.Response.Redirect("/swagger");
                    return Task.CompletedTask;
                });
                endpoints.MapControllers();
            });
        }
    }
}