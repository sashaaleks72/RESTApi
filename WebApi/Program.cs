global using Net7.WebApi.Test.Models;
global using Net7.WebApi.Test.ResponseModels;
global using Net7.WebApi.Test.Data.Entities;
using Net7.WebApi.Test.Services;
using Net7.WebApi.Test.Services.Abstractions;
using Net7.WebApi.Test.Data;
using Microsoft.EntityFrameworkCore;
using Net7.WebApi.Test.Providers.Abstractions;
using Net7.WebApi.Test.Providers;

namespace Net7.WebApi.Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddCors();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddTransient<IProductService, ProductService>();
            builder.Services.AddTransient<IProductProvider, ProductProvider>();
            builder.Services.AddDbContextFactory<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddScoped<IDbContextWrapper<ApplicationDbContext>, DbContextWrapper<ApplicationDbContext>>();
            builder.Services.AddAutoMapper(typeof(Program));
            //builder.Services.AddOptions<ConnectionStringOption>().Bind(builder.Configuration.GetSection("ConnectionStrings"));
            //builder.Services.Configure<ConnectionStringOption>(builder.Configuration);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}