using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using VisualMerchandiser.Models.Domain;
using VisualMerchandiser.Models.Infrastructure.Persistence.Mappings;

namespace VisualMerchandiser.Models.Infrastructure.Persistence
{
    public class VisualMerchandiserContext : IdentityDbContext<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        static VisualMerchandiserContext()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<VisualMerchandiserContext>());
        }

        public VisualMerchandiserContext() : base("VisualMerchandiserContext")
        {

        }

        public VisualMerchandiserContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {

        }

        /// <summary>
        /// Entity Framework Defining DbSets
        /// https://msdn.microsoft.com/en-us/data/jj592675
        /// </summary>
        public DbSet<Shop> Shops => Set<Shop>();
        public DbSet<Visit> Visits => Set<Visit>();

        #region Orphans

        public DbSet<Photo> Photos => Set<Photo>();
        public DbSet<Section> Sections => Set<Section>();

        #endregion        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            //var userConfiguration = modelBuilder.Entity<User>().ToTable("Users");
            //userConfiguration.Ignore(p => p.PhoneNumber);
            //userConfiguration.Ignore(p => p.PhoneNumberConfirmed);
            //userConfiguration.Ignore(p => p.TwoFactorEnabled);
            //modelBuilder.Entity<Role>().ToTable("Roles");
            //modelBuilder.Entity<UserRole>().ToTable("UsersRoles");
            //modelBuilder.Entity<UserClaim>().ToTable("UsersClaims");
            //modelBuilder.Entity<UserLogin>().ToTable("UsersLogins");

            IdentityDbContextOnModelCreating(modelBuilder);

            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof(UserMap)));
        }

        private static void IdentityDbContextOnModelCreating(DbModelBuilder modelBuilder)
        {
            var userConfiguration = modelBuilder.Entity<User>().ToTable("Users");
            userConfiguration.HasMany(u => u.Roles).WithRequired().HasForeignKey(ur => ur.UserId);
            userConfiguration.HasMany(u => u.Claims).WithRequired().HasForeignKey(uc => uc.UserId);
            userConfiguration.HasMany(u => u.Logins).WithRequired().HasForeignKey(u => u.UserId);
            userConfiguration.Property(u => u.UserName).IsRequired().HasMaxLength(256)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("UserNameIndex")
                {
                    IsUnique = true
                }));
            userConfiguration.Property(u => u.Email).HasMaxLength(256);
            modelBuilder.Entity<UserRole>().HasKey(ur => new
            {
                ur.UserId,
                ur.RoleId
            }).ToTable("UserRoles");

            modelBuilder.Entity<UserLogin>().HasKey(ul => new
            {
                ul.LoginProvider,
                ul.ProviderKey,
                ul.UserId
            }).ToTable("UserLogins");

            modelBuilder.Entity<UserClaim>().ToTable("UserClaims");
            var roleConfiguration = modelBuilder.Entity<Role>().ToTable("Roles");
            roleConfiguration.Property(r => r.Name).IsRequired().HasMaxLength(256).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("RoleNameIndex")
            {
                IsUnique = true
            }));
            roleConfiguration.HasMany(r => r.Users).WithRequired().HasForeignKey(ur => ur.RoleId);
        }

        public static VisualMerchandiserContext Create()
        {
            return new VisualMerchandiserContext();
        }

        public static VisualMerchandiserContext Create(string nameOrConnectionString)
        {
            return new VisualMerchandiserContext(nameOrConnectionString);
        }

        private void RemoveOrphans<TEntity>(Func<TEntity, bool> predicate) where TEntity : class
        {
            //var orphans = ChangeTracker.Entries<T>()
            //    .Where(entry => entry.State == EntityState.Modified)
            //    .Select(entry => entry.Entity)
            //    .Where(predicate).ToList();
            //dbSet.RemoveRange(orphans);

            //https://lostechies.com/jimmybogard/2014/05/08/missing-ef-feature-workarounds-cascade-delete-orphans/
            var dbSet = Set<TEntity>();
            var orphans = dbSet.Local.Where(predicate).ToList();
            dbSet.RemoveRange(orphans);
        }

        private void RemoveOrphans()
        {
            //While calling Remove on a DbSet will mark an entity for deletion, calling Remove on a
            //collection navigation property will not. Removing an entity from a collection navigation
            //property will mark the relationship between the two entities as deleted but not the
            //entities themselves.
            RemoveOrphans<Photo>(photo => photo.Section == null);
            RemoveOrphans<Section>(section => section.Visit == null);
        }

        public override int SaveChanges()
        {
            RemoveOrphans();
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                WriteDbEntityValidationException(e);
                throw;
            }
        }

        public override async Task<int> SaveChangesAsync()
        {
            RemoveOrphans();
            try
            {
                return await base.SaveChangesAsync();
            }
            catch (DbEntityValidationException e)
            {
                WriteDbEntityValidationException(e);
                throw;
            }
        }

        private static void WriteDbEntityValidationException(DbEntityValidationException e)
        {
            //An exception of type 'System.Data.Entity.Validation.DbEntityValidationException' occurred in EntityFramework.dll but was not handled in user code
            //Additional information: Validation failed for one or more entities.See 'EntityValidationErrors' property for more details.
            foreach (var dbEntityValidationResult in e.EntityValidationErrors)
            {
                Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                    dbEntityValidationResult.Entry.Entity.GetType().Name, dbEntityValidationResult.Entry.State);
                foreach (var dbValidationError in dbEntityValidationResult.ValidationErrors)
                {
                    Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"", dbValidationError.PropertyName, dbValidationError.ErrorMessage);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
