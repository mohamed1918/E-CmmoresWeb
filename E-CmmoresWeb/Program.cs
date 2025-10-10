
using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Contexts;

namespace E_CmmoresWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
                    
            builder.Services.AddScoped<IDataSeeding, IDataSeeding>();

            #endregion

            var app = builder.Build();

            #region Data Seeding
            var Scope = app.Services.CreateScope();

            var seed = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();

            seed.DataSeed();
            #endregion

            #region Configure the HTTP request pipeline.
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
