using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.BusinessEntities;
using ProductCatalog.Contracts;
using ProductCatalog.LoggerService;
using ProductCatalog.Repository;
using Swashbuckle.AspNetCore.Swagger;

namespace ProductCatalog.Services.Extensions
{
    /// <summary>
    ///   Configure Service Extensions class
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Configure Cors
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }

        /// <summary>
        /// Configure Swagger Integration
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureSwaggerIntegration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Product Catalog API Doc.",
                    Version = "v1",

                    // You can also set Description, Contact, License, TOS...
                });

                //   c.AddSecurityDefinition("Bearer", new ApiKeyScheme { In = "header", Description = "Please enter JWT with Bearer into field", Name = "Authorization", Type = "apiKey" });
                //   c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                //{
                //    { "Bearer", Enumerable.Empty<string>() },
                //});
                // Configure Swagger to use the xml documentation file
                var xmlFile = Path.ChangeExtension(typeof(Startup).Assembly.Location, ".xml");
                c.IncludeXmlComments(xmlFile);
            });
        }

        /// <summary>
        ///  Configure IIS Integration
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }
        /// <summary>
        /// Configure Logger Service
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
        /// <summary>
        /// Configure MySql Context
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ProductRepositoryContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
        }
        /// <summary>
        /// Configure Repository Wrapper
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }
    }
}
