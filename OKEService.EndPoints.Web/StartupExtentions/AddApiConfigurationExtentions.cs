using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using FluentValidation.AspNetCore;
using OKEService.EndPoints.Web.Filters;
using OKEService.EndPoints.Web.Middlewares.ApiExceptionHandler;
using OKEService.Utilities.Configurations;

namespace OKEService.EndPoints.Web.StartupExtentions
{
    public static class AddApiConfigurationExtentions
    {
        public static IServiceCollection AddOKEServiceApiServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            var _OKEserviceConfigurations = new OKEServiceConfigurationOptions();
            configuration.GetSection(_OKEserviceConfigurations.SectionName).Bind(_OKEserviceConfigurations);
            services.AddSingleton(_OKEserviceConfigurations);
            services.AddScoped<ValidateModelStateAttribute>();
            services.AddControllers(options =>
            {
                options.Filters.AddService<ValidateModelStateAttribute>();
                options.Filters.Add(typeof(TrackActionPerformanceFilter));
            }).AddFluentValidation();

            services.AddOKEServiceDependencies(_OKEserviceConfigurations.AssmblyNameForLoad.Split(','));

            AddSwagger(services);
            return services;
        }

        private static void AddSwagger(IServiceCollection services)
        {
            var _OKEserviceConfigurations = services.BuildServiceProvider().GetService<OKEServiceConfigurationOptions>();
            if (_OKEserviceConfigurations?.Swagger?.Enabled == true && _OKEserviceConfigurations.Swagger.SwaggerDoc != null)
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc(_OKEserviceConfigurations.Swagger.SwaggerDoc.Name, new OpenApiInfo { Title = _OKEserviceConfigurations.Swagger.SwaggerDoc.Title, Version = _OKEserviceConfigurations.Swagger.SwaggerDoc.Version });
                });
            }
        }
        public static void UseOKEServiceApiConfigure(this IApplicationBuilder app, OKEServiceConfigurationOptions configuration, IWebHostEnvironment env)
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
            if (configuration.Swagger != null && configuration.Swagger.SwaggerDoc != null)
            {

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint(configuration.Swagger.SwaggerDoc.URL, configuration.Swagger.SwaggerDoc.Title);
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }




    }
}
