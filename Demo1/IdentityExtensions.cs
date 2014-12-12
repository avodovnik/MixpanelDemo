using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNet.Identity;

namespace Demo1
{
    public static class IdentityExtensions
    {
        public static string GetUserEmail(this IIdentity identity)
        {
            if (identity is ClaimsIdentity)
                return (identity as ClaimsIdentity).GetUserEmail();
            return "Invalid Identity";
        }

        public static string GetUserEmail(this ClaimsIdentity claimsIdentity)
        {
            return claimsIdentity.FindFirstValue(ClaimTypes.Email);
        }
    }
}