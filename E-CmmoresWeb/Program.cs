
using Azure;
using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using E_CmmoresWeb.CustomMiddleWares;
using E_CmmoresWeb.Extensions;
using E_CmmoresWeb.Factories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data.Contexts;
using Persistence.Identity;
using Persistence.Repositories;
using Service;
using Service.MappingProfiles;
using ServiceAbstration;
using Shared.ErrorModels;
using System.Threading.Tasks;

namespace E_CmmoresWeb
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.
            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddSwaggerServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddAplicationServices();
            //builder.WebHost.UseWebRoot(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"));
            builder.Services.AddWebApllicationServices();
            builder.Services.AddJWTServices(builder.Configuration);


            #endregion

            var app = builder.Build();

            await app.SeedDataAsync();

            #region Configure the HTTP request pipeline.
            // Configure the HTTP request pipeline
            //app.Use(async (RequestContext, NextMiddleWare) =>
            //{
            //    Console.WriteLine("Request Under Processing");
            //    await NextMiddleWare.Invoke();
            //    Console.WriteLine("Waiting Processing");
            //    Console.WriteLine(RequestContext.Response.Body);
            //});


            app.UseCustomExceptionMiddlWare();
            if (app.Environment.IsDevelopment())
            {
               app.UseSwaggerMiddleWares();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
