using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using Microsoft.AspNet.Identity;
using VisualMerchandiser.Models.Application;
using VisualMerchandiser.Models.Application.AspNetIdentity;
using VisualMerchandiser.Models.Domain;
using VisualMerchandiser.Models.Infrastructure.Persistence;
using Xunit;
using Xunit.Abstractions;

namespace VisualMerchandiser.Models
{
    public class VisualMerchandiserShould : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly VisualMerchandiserContext _context;
        private readonly UserStore _userStore;
        private readonly ApplicationUserManager _userManager;
        private readonly RoleStore _roleStore;
        private readonly ApplicationRoleUserManager _roleManager;

        public VisualMerchandiserShould(ITestOutputHelper output)
        {
            _output = output;
            _context = VisualMerchandiserContext.Create();
            _context.Database.Initialize(true);
            _userStore = new UserStore(_context);
            _userManager = ApplicationUserManager.Create(_userStore);
            _roleStore = new RoleStore(_context);
            _roleManager = ApplicationRoleUserManager.Create(_roleStore);
        }

        private void PrintIdentityResult(IdentityResult result)
        {
            _output.WriteLine(result.Succeeded.ToString());
            foreach (var error in result.Errors)
            {
                _output.WriteLine(error);
            }
        }

        [Fact]
        public async void create_user()
        {
            using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                const string roleName = "Administrador";

                var result = await _roleManager.CreateAsync(new Role(roleName));
                PrintIdentityResult(result);

                var user = new User
                {
                    FirstName = "Sergio",
                    LastName = "León",
                    UserName = "sergio.leon",
                    Email = "panicoenlaxbox@gmail.com"
                };
                result = await _userManager.CreateAsync(user, "Sergio@2");
                PrintIdentityResult(result);

                result = await _userManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.PostalCode, "28054"));
                result = await _userManager.AddClaimAsync(user.Id, new Claim("Acme:Department", "Develop"));
                PrintIdentityResult(result);

                result = await _userManager.AddToRoleAsync(user.Id, roleName);
                PrintIdentityResult(result);

                tran.Complete();
            }
            var identity = await _userManager.CreateIdentityAsync(await _userManager.FindByNameAsync("sergio.leon"), DefaultAuthenticationTypes.ApplicationCookie);
        }

        [Fact]
        public async void RemovePhoto()
        {
            var user = new User
            {
                FirstName = "Sergio",
                LastName = "León",
                UserName = "sergio.leon",
                Email = "panicoenlaxbox@gmail.com"
            };
            IdentityResult result = await _userManager.CreateAsync(user, "Sergio@2");
            var shop = new Shop() { Name = "Foo" };
            _context.Shops.Add(shop);
            user = _context.Users.Find(user.Id);
            user.ScheduleVisit(shop, DateTime.UtcNow);
            var visit = user.Visits.First();
            visit.CreateSection("Section 1");
            var section = visit.Sections.First();
            byte[] content = Encoding.Default.GetBytes("sergio");
            visit.AddPhoto(section, content, PhotoMoment.After);
            await _context.SaveChangesAsync();

            visit.Comment(user, "hola");
            _context.SaveChanges();

            var photo = section.Photos.First();
            visit.RemovePhoto(photo);
            _context.ChangeTracker.DetectChanges();
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
            _userStore?.Dispose();
            _userManager?.Dispose();
            _roleStore?.Dispose();
            _roleManager?.Dispose();
        }
    }
}
