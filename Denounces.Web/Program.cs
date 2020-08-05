using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Denounces.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var host = CreateWebHostBuilder(args).Build();
            ////  RunSeeding(host);
            //host.Run();
            CreateHostBuilder(args).Build().Run();
        }

        //private static void RunSeeding(IWebHost host)
        //{
        //    var scopeFactory = host.Services.GetService<IServiceScopeFactory>();
        //    using (var scope = scopeFactory.CreateScope())
        //    {
        //        var seeder = scope.ServiceProvider.GetService<SeedDb>();
        //        seeder.SeedAsync().Wait();
        //    }
        //}

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
