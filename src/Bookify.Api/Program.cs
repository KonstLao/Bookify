
using Azure;
using Bogus;
using Bookify.Api.Extensions;
using Bookify.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bookify.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                // Поддержка DateOnly на уровне swagger
                options.UseDateOnlyTimeOnlyStringConverters();
                var basePath = AppContext.BaseDirectory;
                var xmlPathApi = Path.Combine(basePath, "Bookify.Api.xml");
                options.IncludeXmlComments(xmlPathApi);
                var xmlPathApplication = Path.Combine(basePath, "Bookify.Application.xml");
                options.IncludeXmlComments(xmlPathApplication);
                //options.SwaggerDoc("v1", new OpenApiInfo
                //{
                //    //
                //});
                //options.OperationFilter<AddDefaultParameter>();

            });

            var app = builder.Build();
            app.ApplyMigrations();
            await app.SeedData();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI((c =>
                {

                }));
            }

            app.MapControllers();

            await app.RunAsync();
        }

    }
}
