using Microsoft.AspNet.Identity;
using VisualMerchandiser.Models.Domain;

namespace VisualMerchandiser.Models.Application.AspNetIdentity
{
    class ApplicationUserManager : UserManager<User, int>
    {
        public ApplicationUserManager(IUserStore<User, int> store) : base(store)
        {
            ClaimsIdentityFactory = new ApplicationClaimsIdentityFactory();
        }

        public static ApplicationUserManager Create(IUserStore<User, int> store)
        {
            var manager = new ApplicationUserManager(store);
            manager.UserValidator = new UserValidator<User, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            return manager;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}