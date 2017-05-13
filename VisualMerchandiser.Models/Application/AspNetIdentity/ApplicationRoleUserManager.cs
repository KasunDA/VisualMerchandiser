using Microsoft.AspNet.Identity;
using VisualMerchandiser.Models.Domain;

namespace VisualMerchandiser.Models.Application.AspNetIdentity
{
    class ApplicationRoleUserManager : RoleManager<Role, int>
    {
        public ApplicationRoleUserManager(IRoleStore<Role, int> store) : base(store)
        {

        }

        public static ApplicationRoleUserManager Create(IRoleStore<Role, int> store)
        {
            return new ApplicationRoleUserManager(store);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}