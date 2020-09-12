using System;
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
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CoffeeMapServer.Infrastructures;
using Microsoft.AspNetCore.CookiePolicy;
using System.Threading.Tasks;

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
            // services.AddRazorPages();
            //services.AddSwaggerGen();
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            //});
            services.AddDbContext<CoffeeDbContext>(options => options.UseSqlServer(connection));
            services.AddTransient<IRoasterRepository, RoasterRepository>();
            services.AddTransient<IAddessRepository, AddressRepository>();
            services.AddTransient<ITagRepository, TagRepository>();
            services.AddTransient<IRoasterTagRepository, RoasterTagRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // ��������, ����� �� �������������� �������� ��� ��������� ������
                    ValidateIssuer = true,
                    // ������, �������������� ��������
                    ValidIssuer = AuthOptions.ISSUER,

                    // ����� �� �������������� ����������� ������
                    ValidateAudience = true,
                    // ��������� ����������� ������
                    ValidAudience = AuthOptions.AUDIENCE,
                    // ����� �� �������������� ����� �������������
                    ValidateLifetime = true,

                    // ��������� ����� ������������
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    // ��������� ����� ������������
                    ValidateIssuerSigningKey = true,
                };
            });
            services.AddAuthorization(config =>
            {
                config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
                config.AddPolicy(Policies.Master, Policies.MasterPolicy());
            });
            services.AddRazorPages().AddRazorPagesOptions(options =>
            {
                options.Conventions.AuthorizeFolder("/Admin/AddressesViews/");
                options.Conventions.AuthorizeFolder("/Admin/RoasterViews/");
                options.Conventions.AuthorizeFolder("/Admin/RoasterAddress/");
                options.Conventions.AuthorizeFolder("/Admin/TagViews/");
                // options.Conventions.AllowAnonymousToPage("/Admin/AuthorizationViews/Login");
            });
            //services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseStatusCodePages(async context =>
            {
                var response = context.HttpContext.Response;
                if (response.StatusCode == (int)System.Net.HttpStatusCode.Unauthorized ||
                response.StatusCode == (int)System.Net.HttpStatusCode.Forbidden)
                    response.Redirect("/api/Login");
            });
            // prepare token to insert into cookie
            //app.UseSwagger();
            app.Use(async (context, next) =>
            {
                var token = context.Request.Cookies[".AspNetCore.Meta.Metadta"];
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
                //Secure = CookieSecurePolicy.Always
            });
            //Enable middleware to serve swagger - ui(HTML, JS, CSS, etc.),
            //specifying the Swagger JSON endpoint.
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            //});
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });

        }
    }
}
