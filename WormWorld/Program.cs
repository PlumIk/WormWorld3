using System;
using System.Diagnostics;
using System.IO;
using AnyTests.ForUnitTests.BaseIn;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace WormWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(null).Build().Run();
        }

        static IHostBuilder CreateHostBuilder(string[] args)
        {
            var opt = new DbContextOptionsBuilder<DataBase.ApplicationContext>().Options;
            DataBase.ApplicationContext db = new DataBase.ApplicationContext(opt);

            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddHostedService<SimHost>();

                    services.AddScoped<FileHost>();
                    services.AddScoped<FoodHost>();
                    services.AddScoped<WormHost>();
                    services.AddScoped<NameHost>();
                    services.AddScoped<WorldLogic>();
                    services.AddSingleton(db);
                });
        }

    }
    
    class DbConfig
    {
        public DataBase.ApplicationContext Db{ get; set; }
    }
}