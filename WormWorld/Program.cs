using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WormWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(null).Build().Run();
        }
        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddHostedService<FoodHost>();
                    services.AddHostedService<WormHost>();
                    services.AddHostedService<FileHost>();
                    services.AddHostedService<NameHost>();
                    services.AddHostedService<ForEnd>();
                    services.AddSingleton<WorldLogic>();
                });
    }
}