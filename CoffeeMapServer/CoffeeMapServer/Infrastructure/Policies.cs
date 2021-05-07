using Microsoft.AspNetCore.Authorization;

namespace CoffeeMapServer.Infrastructures
{
    public class Policies
    {
        //TODO: Spread over solution
        public const string Admin = "Admin";
        public const string Master = "Master";

        public static AuthorizationPolicy AdminPolicy() 
            => new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin).Build();

        public static AuthorizationPolicy MasterPolicy() 
            => new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Master).Build();
    }
}