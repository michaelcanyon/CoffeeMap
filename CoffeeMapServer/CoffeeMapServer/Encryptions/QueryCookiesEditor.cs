using CoffeeMapServer.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CoffeeMapServer.Encryptions
{
    public static class QueryCookiesEditor
    {
        public static async Task SetUserCookies(User user, string token, HttpContext context)
        {
            context.Response.Cookies.Append(".AspNetCore.Meta.Metadta", token);
            context.Response.Cookies.Append(".AspNetCore.Meta.Metadta.id", user.Id.ToString());
            context.Response.Cookies.Append(".AspNetCore.Meta.Metadta.nickname", user.Login);
            context.Response.Cookies.Append(".AspNetCore.Meta.Metadta.role", user.role);
        }
        public static async Task ClearCookies(HttpContext context)
        {
            context.Response.Cookies.Append(".AspNetCore.Meta.Metadta", "");
            context.Response.Cookies.Append(".AspNetCore.Meta.Metadta.id", "");
            context.Response.Cookies.Append(".AspNetCore.Meta.Metadta.nickname", "");
            context.Response.Cookies.Append(".AspNetCore.Meta.Metadta.role", "");
        }
    }
}
