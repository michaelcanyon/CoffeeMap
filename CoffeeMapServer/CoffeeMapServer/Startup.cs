using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMapServer.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Infrastructures.Repositories;
using Microsoft.OpenApi.Models;
using CoffeeMapServer.Infrastructures.Repositories.Intermediary_repositories;

namespace CoffeeMapServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            IConfigurationSection connString =  Configuration.GetSection("ConnectionString");
            string connection = connString.GetSection("Connection").Value;
            services.AddControllersWithViews();
            services.AddDbContext<CoffeeDbContext>(options => options.UseSqlServer(connection));
            services.AddCors();
            services.AddRazorPages();
            services.AddTransient<IRoasterRepository, RoasterRepository>();
            services.AddTransient<IAddessRepository, AddressRepository>();
            services.AddTransient<ITagRepository, TagRepository>();
            services.AddTransient<IRoasterTagRepository, RoasterTagRepository>();
            services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();   
            app.UseStaticFiles();

            //Enable middleware to serve swagger - ui(HTML, JS, CSS, etc.),
             //specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

        }
    }
}
