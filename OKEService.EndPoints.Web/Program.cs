using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;

namespace OKEService.EndPoints.Web
{
    public class OKEServiceProgram
    {
        public IHostBuilder Main(string[] args, Type startUp, params string[] appSettingFiles)
        {

            if (appSettingFiles == null || !appSettingFiles.Any())
            {
                appSettingFiles = new string[] { "appsettings.json" };
            }
            var configBuilder = new ConfigurationBuilder();
            foreach (var item in appSettingFiles)
            {
                configBuilder.AddJsonFile(item);
            }
            var configuration = configBuilder.Build();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
            try
            {
                Log.Information("Starting web host");
                var hostBuilder = CreateHostBuilder(args, startUp, appSettingFiles);
                return hostBuilder;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return null;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private IHostBuilder CreateHostBuilder(string[] args, Type startUp, params string[] appSettingFiles) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((ctx, config) =>
            {
                foreach (var item in appSettingFiles)
                {
                    config.AddJsonFile(item, true);
                }
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup(startUp);
            })
            .UseSerilog();



    }
}
