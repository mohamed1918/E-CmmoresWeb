using E_CmmoresWeb.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace E_CmmoresWeb.Extensions
{
    public static class ServiceRedistration
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection Services)
        {
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    Description = "Enter 'Bearer' followed by space and your Token",
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme,
                            }
                        },
                        new string[] {}
                    }

                });
            });

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

        public static IServiceCollection AddJWTServices(this IServiceCollection Services, IConfiguration _configuration)
        {
            Services.AddAuthentication((config) =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer((options) =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                   ValidateIssuer = true,
                   ValidIssuer = _configuration["JwtSettings:Issuer"],

                   ValidateAudience = true,
                   ValidAudience = _configuration["JwtSettings:Audience"],

                   ValidateLifetime = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"])),
                };

                
            });


            return Services;

        }
    }
}
