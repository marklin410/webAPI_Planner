 using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DietPlanner
{
    public class Program
    {
        Logger logger = new LoggerConfiguration()
               .WriteTo.Sentry("https://ab19f59436da43e0a070d43df3c942c0@o828966.ingest.sentry.io/5811828")
               .WriteTo.Console()
               .Enrich.FromLogContext()
               .CreateLogger();
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseSentry();
                    webBuilder.ConfigureLogging((context, loggingBuilder)=>
                    {
                        loggingBuilder.AddSerilog();
                    });

                    webBuilder.UseSerilog();
                    webBuilder.UseStartup<Startup>();

                });
    }
}
