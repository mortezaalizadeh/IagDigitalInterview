using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using VehicleSummary.Api.Services.ConfigReader;
using VehicleSummary.Api.Services.VehicleSummary;
using VehicleSummary.Contracts;

namespace VehicleSummary.Api
{
    public class Startup
    {
        private readonly ILogger<Startup> _logger;

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger =
                logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _logger.LogInformation(LoggingEvents.Startup, "ConfigureServices is configuring required services...");

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            _logger.LogInformation(LoggingEvents.Startup, "Bootstrapping the DI ...");

            services.AddScoped<IVehicleSummaryService, VehicleSummaryService>();
            services.AddScoped<IConfigReaderService, ConfigReaderService>();

            _logger.LogInformation(LoggingEvents.Startup, "Successfully finished configuring the service.");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

            app.ConfigureExceptionHandler(_logger);

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}