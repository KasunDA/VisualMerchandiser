using System.Security.Claims;

namespace VisualMerchandiser.Models.Application.AspNetIdentity
{
    public static class ClaimsIdentityExtensions
    {
        public static void SaveClaim(this ClaimsIdentity identity, string key, string value)
        {
            var claim = identity.FindFirst(key);
            if (claim != null)
            {
                identity.RemoveClaim(claim);
            }
            identity.AddClaim(new Claim(key, value));
        }

        public static string GetClaimValue(this ClaimsIdentity identity, string key)
        {
            return identity.FindFirst(key)?.Value;
        }
    }
}