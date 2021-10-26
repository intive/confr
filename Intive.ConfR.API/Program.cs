using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Intive.ConfR.Logging;
using Intive.ConfR.Persistence;
using Microsoft.Extensions.Configuration;
using NLog.Web;

namespace Intive.ConfR.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webHost = CreateWebHostBuilder(args).Build();

            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                ILoggerManager logger = new LoggerManager();

                try
                {
                    var context = services.GetService<ConfRContext>();
                    context.Database.Migrate();
                    ConfrInitializer.Initialize(context);
                }
                catch (Exception e)
                {
                    logger.LogError(String.Concat(e.Message, "\n", e.StackTrace));
                }
            }

            webHost.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                    config.AddEnvironmentVariables();
                })
                .CaptureStartupErrors(true)
                .UseSetting("detailedErrors", "true")
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                    #if DEBUG
                    logging.SetMinimumLevel(LogLevel.Information);
                    #endif
                })
                .UseNLog();
    }
}
