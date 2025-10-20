using Microsoft.Extensions.DependencyInjection;
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
            Services.AddScoped<IServiceManager, ServiceManager>();

            return Services;
        }
    }
}
