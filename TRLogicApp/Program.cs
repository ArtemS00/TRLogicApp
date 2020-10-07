using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TRLogicApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var life = host.Services.GetRequiredService<IHostApplicationLifetime>();
            life.ApplicationStopping.Register(() =>
            {
                // Graceful shutdown
                Console.WriteLine("Application is stopping. Wait 10 seconds.");
                Thread.Sleep(10000);
            });
            life.ApplicationStopped.Register(() =>
                Console.WriteLine("Application has been stopped."));

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
