using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace CoffeeMapServer.Middlewares
{
    public static class AuthMiddlewares
    {
        public static IApplicationBuilder SupplyResponceWithToken(this IApplicationBuilder app)
        => app.Use(async (context, next) =>
        {
            var token = context.Request.Cookies[".AspNetCore.Meta.Metadata"];
            if (!string.IsNullOrEmpty(token))
                context.Request.Headers.Append("Authorization", "Bearer " + token);

            await next();
        });

        public static IApplicationBuilder UnauthorizedLogin(this IApplicationBuilder app)
        => app.UseStatusCodePages(async context =>
        {
            var response = context.HttpContext.Response;
            if (response.StatusCode == (int)System.Net.HttpStatusCode.Unauthorized)
                response.Redirect("/Login");
        });
    }
}
