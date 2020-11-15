using CoffeeMapServer.Models;
using Microsoft.AspNetCore.Http;

namespace CoffeeMapServer.Encryptions
{
    public static class QueryCookiesEditor
    {
        public static void SetUserCookies(User user, string token, HttpContext context)
        {
            context.Response.Cookies.Append(".AspNetCore.Meta.Metadata", token);
            context.Response.Cookies.Append(".AspNetCore.Meta.Metadata.id", user.Id.ToString());
            context.Response.Cookies.Append(".AspNetCore.Meta.Metadata.nickname", user.Login);
            context.Response.Cookies.Append(".AspNetCore.Meta.Metadata.role", user.Role);
        }

        public static void ClearCookies(HttpContext context)
        {
            context.Response.Cookies.Append(".AspNetCore.Meta.Metadata", "");
            context.Response.Cookies.Append(".AspNetCore.Meta.Metadata.id", "");
            context.Response.Cookies.Append(".AspNetCore.Meta.Metadata.nickname", "");
            context.Response.Cookies.Append(".AspNetCore.Meta.Metadata.role", "");
        }
    }
}