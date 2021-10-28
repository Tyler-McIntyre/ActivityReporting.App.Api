using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using ActivityReporting.App.Api.Interfaces;

namespace ActivityReporting.App.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Log.Logger = new LoggerConfiguration()
            .ReadFrom
            .Configuration(Configuration)
            .Enrich
            .FromLogContext()
            .WriteTo.File($"../logs/" +
            $"{DateTime.Now.ToShortDateString().Replace("/", "")}.log")
            .CreateLogger();

            Log.Logger.Information("Application Starting");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ActivityReporting.App.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ActivityReporting.App.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            IHost host = Host.CreateDefaultBuilder().ConfigureServices((context, service) => {
                service.AddSingleton<InMemDatabase>();
            })
                .UseSerilog()
                .Build();

            _ = ActivatorUtilities.CreateInstance<InMemDatabase>(host.Services);
        }
    }
}
