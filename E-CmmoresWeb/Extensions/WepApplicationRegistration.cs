using DomainLayer.Contracts;
using E_CmmoresWeb.CustomMiddleWares;
using Microsoft.AspNetCore.Builder;
using System.Threading.Tasks;

namespace E_CmmoresWeb.Extensions
{
    public static class WepApplicationRegistration
    {
        public static async Task SeedDataAsync(this WebApplication app)
        {
            var Scope = app.Services.CreateScope();

            var seed = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();

            await seed.DataSeedAsync();
        }

        public static IApplicationBuilder UseCustomExceptionMiddlWare (this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();
            return app;
        }

        public static IApplicationBuilder UseSwaggerMiddleWares (this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }

    }
}
