using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace OcelotApiGw
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        //    public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureAppConfiguration((hostingContext, config) =>
        //        {
        //            config.AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true);
        //        })
        //        .UseSerilog(SeriLogger.Configure)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });
        //}

        //    public static IHostBuilder CreateHostBuilder(string[] args) =>
        //        Host.CreateDefaultBuilder(args)
        //                 .ConfigureAppConfiguration((hostingContext, config) =>
        //                 {
        //                     config.AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true);
        //                 })
        //            .ConfigureWebHostDefaults(webBuilder =>
        //            {
        //                webBuilder.UseStartup<Startup>();
        //            }).ConfigureLogging((hositingContect, loggingBulider) =>
        //            {
        //                loggingBulider.AddConfiguration(hositingContect
        //                    .Configuration.GetSection("Logging"));
        //                loggingBulider.AddConsole();
        //                loggingBulider.AddDebug();
        //            });
        //}

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStartup<Startup>();
               })
           .ConfigureAppConfiguration((hostingContext, config) =>
           {
               config.AddJsonFile($"osl.json");

               // for local test use below one, multi env json file not worked
               //config.AddJsonFile($"configuration.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true);
               // https://github.com/ThreeMammals/Ocelot/issues/249
           });
    }
    }
