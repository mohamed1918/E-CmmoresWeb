using E_CmmoresWeb.Factories;
using Microsoft.AspNetCore.Mvc;

namespace E_CmmoresWeb.Extensions
{
    public static class ServiceRedistration
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection Services)
        {
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();

            return Services;
        }

        public static IServiceCollection AddWebApllicationServices(this IServiceCollection Services)
        {
            Services.Configure<ApiBehaviorOptions>((options) =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationErrorsResponse;

            });
            return Services;
        }
    }
}
