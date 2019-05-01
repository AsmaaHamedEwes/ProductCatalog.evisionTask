using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Owin;
using NLog;
using Owin;
using ProductCatalog.Contracts;
using ProductCatalog.LoggerService;

[assembly: OwinStartup(typeof(ProductCatalog.Repository.Startup))]

namespace ProductCatalog.Repository
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {

            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
        
    }
}
