using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using VisualMerchandiser.Models.Domain;

namespace VisualMerchandiser.Models.Application.AspNetIdentity
{
    class ApplicationClaimsIdentityFactory : ClaimsIdentityFactory<User, int>
    {
        public override async Task<ClaimsIdentity> CreateAsync(UserManager<User, int> manager, User user, string authenticationType)
        {
            var identity = await base.CreateAsync(manager, user, authenticationType);
            identity.SaveClaim("Acme:Key", "Value");
            return identity;
        }
    }
}