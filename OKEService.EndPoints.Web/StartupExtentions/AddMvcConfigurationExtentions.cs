﻿using OKEService.EndPoints.Web.Filters;
using OKEService.EndPoints.Web.Middlewares.ApiExceptionHandler;
using OKEService.Utilities.Configurations;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;

namespace OKEService.EndPoints.Web.StartupExtentions
{
    public static class AddMvcConfigurationExtentions
    {
        public static IServiceCollection AddZaminMvcServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            var _zaminConfigurations = new OKEServiceConfigurationOptions();
            configuration.GetSection(_zaminConfigurations.SectionName).Bind(_zaminConfigurations);
            services.AddSingleton(_zaminConfigurations);
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(typeof(TrackActionPerformanceFilter));
            }).AddRazorRuntimeCompilation()
            .AddFluentValidation();

            services.AddOKEServiceDependencies(_zaminConfigurations.AssmblyNameForLoad.Split(','));

            return services;
        }

        public static void UseZaminMvcConfigure(this IApplicationBuilder app, Action<IEndpointRouteBuilder> configur, OKEServiceConfigurationOptions configuration, IWebHostEnvironment env)
        {
            app.UseApiExceptionHandler(options =>
            {
                options.AddResponseDetails = (context, ex, error) =>
                {
                    if (ex.GetType().Name == typeof(SqlException).Name)
                    {
                        error.Detail = "Exception was a database exception!";
                    }
                };
                options.DetermineLogLevel = ex =>
                {
                    if (ex.Message.StartsWith("cannot open database", StringComparison.InvariantCultureIgnoreCase) ||
                        ex.Message.StartsWith("a network-related", StringComparison.InvariantCultureIgnoreCase))
                    {
                        return LogLevel.Critical;
                    }
                    return LogLevel.Error;
                };
            });

            app.UseStatusCodePages();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            if (configuration?.Session?.Enable == true)
                app.UseSession();

            app.UseEndpoints(configur);
        }
    }
}
