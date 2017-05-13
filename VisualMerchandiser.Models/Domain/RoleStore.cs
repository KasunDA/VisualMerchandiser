using Microsoft.AspNet.Identity.EntityFramework;
using VisualMerchandiser.Models.Infrastructure.Persistence;

namespace VisualMerchandiser.Models.Domain
{
    class RoleStore : RoleStore<Role, int, UserRole>
    {
        public RoleStore(VisualMerchandiserContext context) : base(context)
        {
        }
    }
}