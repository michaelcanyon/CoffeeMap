using CoffeeMapServer.EF;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Infrastructures.Repositories;
using CoffeeMapServer.Services;
using CoffeeMapServer.Services.Interfaces;
using CoffeeMapServer.Services.Interfaces.Admin;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

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
            IConfigurationSection connString = Configuration.GetSection("ConnectionString");
            string connection = connString.GetSection("Connection").Value;
            services.AddRazorPages();
            services.AddCors();
            services.AddControllersWithViews();
            services.AddDbContext<CoffeeDbContext>(options => options.UseSqlServer(connection));
            services.AddTransient<IRoasterRepository, RoasterRepository>();
            services.AddTransient<IAddressRepository, AddressRepository>();
            services.AddTransient<ITagRepository, TagRepository>();
            services.AddTransient<IRoasterTagRepository, RoasterTagRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoasterRequestRepository, RoasterRequestRepository>();
            services.AddTransient<IRoasterService, RoasterService>();
            services.AddTransient<IRoasterAdminService, RoasterAdminService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IAddressService, AddressService>();
            services.AddTransient<ITagService, TagService>();
            services.AddTransient<IRoasterAddressConnectionService, RoasterAddressConnectionService>();
            services.AddTransient<IRoasterRequestService, RoasterRequestService>();
            services.AddTransient<IIdentityGeneratorService, IdentityGeneratorService>();
            services.ConfigureAuth();
            services.AddRazorPages().AddRazorPagesOptions(options =>
            {
                options.Conventions.AuthorizeFolder("/Admin/AddressesViews/");
                options.Conventions.AuthorizeFolder("/Admin/RoasterViews/");
                options.Conventions.AuthorizeFolder("/Admin/RoasterAddress/");
                options.Conventions.AuthorizeFolder("/Admin/TagViews/");
                options.Conventions.AuthorizeFolder("/Admin/Account/");
                options.Conventions.AuthorizeFolder("/Admin/RoasterRequestViews/");
                // options.Conventions.AllowAnonymousToPage("/Admin/AuthorizationViews/Login");
            });
            //services.AddMvc();
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
            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(origin => origin.AllowAnyOrigin());
            app.UseStaticFiles();
            app.UseStatusCodePages(async context =>
            {
                var response = context.HttpContext.Response;
                if (response.StatusCode == (int)System.Net.HttpStatusCode.Unauthorized)
                    response.Redirect("/Login");
            });
            // prepare token to insert into cookie
            app.UseSwagger();
            app.Use(async (context, next) =>
            {
                var token = context.Request.Cookies[".AspNetCore.Meta.Metadata"];
                if (!string.IsNullOrEmpty(token))
                    context.Request.Headers.Append("Authorization", "Bearer " + token);

                await next();
            });
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                //   MinimumSameSitePolicy = SameSiteMode.Strict,
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always
            });
            //Enable middleware to serve swagger - ui(HTML, JS, CSS, etc.),
            //specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });

        }
    }
}
