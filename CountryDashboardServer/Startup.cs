using System;
using System.Collections.Generic;
using System.Linq;
using CountryDashboardServer.Components.Database;
using CountryDashboardServer.Components.Services;
using CountryDashboardServer.Interfaces;
using CountryDashboardServer.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace CountryDashboardServer
{
    public class Startup
    {
        private string connectionString;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionString = Configuration.GetConnectionString("CountryDashboard");
            ApiHelper.InitializeClient();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Country Dashboard Api", Version = "v1" });
            });
            services.AddTransient<ICountryService, CountryService>();
            services.AddDbContext<DbContexts>(c => c.UseSqlServer(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Country Dashboard Api");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
