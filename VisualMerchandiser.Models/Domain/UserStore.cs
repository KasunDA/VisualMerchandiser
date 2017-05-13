using Microsoft.AspNet.Identity.EntityFramework;
using VisualMerchandiser.Models.Infrastructure.Persistence;

namespace VisualMerchandiser.Models.Domain
{
    class UserStore : UserStore<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        public UserStore(VisualMerchandiserContext context) : base(context)
        {
        }
    }
}