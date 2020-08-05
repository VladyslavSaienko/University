using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog.Web;
using University.API.Contexts;

namespace University.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder
                    .ConfigureNLog("nlog.config")
                    .GetCurrentClassLogger();
            try
            {
                logger.Info("Initializing application...");
                var host = CreateHostBuilder(args).Build();

                using (var scope = host.Services.CreateScope())
                {
                    try
                    {
                        var context = scope.ServiceProvider.GetService<UniversityContext>();

                        context.Database.EnsureDeleted();
                        context.Database.Migrate();
                    }
                    catch (Exception ex)
                    {

                        logger.Error(ex, "An error occured while migrating the database");
                    }

                    host.Run();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Application stopped due to exception");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .UseStartup<Startup>()
                    .UseNLog();
                });
    }
}
