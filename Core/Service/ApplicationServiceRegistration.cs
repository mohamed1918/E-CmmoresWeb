﻿using Microsoft.Extensions.DependencyInjection;
using Service.MappingProfiles;
using ServiceAbstration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection Services)
        {
            Services.AddAutoMapper(config => config.AddProfile(new ProductProfile()), typeof(Service.AssemblyReference).Assembly);
            Services.AddScoped<IServiceManager, ServiceManagerWithFactoryDelegate>();
            //Services.AddScoped<IServiceManager, ServiceManager>();
            //Services.AddKeyedScoped<IServiceManager, ServiceManager>("Lazy");
            //Services.AddKeyedScoped<IServiceManager, ServiceManagerWithFactoryDelegate>("FactoryDelegate");

            Services.AddScoped<IProductService, ProductService>();
            Services.AddScoped<Func<IProductService>>(provider =>
            {
                return () => provider.GetRequiredService<IProductService>()!;
            });

            Services.AddScoped<IBasketService, BasketService>();
            Services.AddScoped<Func<IBasketService>>(provider =>
            {
                return () => provider.GetRequiredService<IBasketService>()!;
            });

            Services.AddScoped<IAuthenticationService, AuthenticationService>();
            Services.AddScoped<Func<IAuthenticationService>>(provider =>
            {
                return () => provider.GetRequiredService<IAuthenticationService>()!;
            });
            Services.AddScoped<IOrderService, OrderService>();
            Services.AddScoped<Func<IOrderService>>(provider =>
            {
                return () => provider.GetRequiredService<IOrderService>()!;
            });

            Services.AddScoped<ICacheService, CacheService>();

            return Services;
        }
    }
}
