using System.Data.Entity;
using System.Threading.Tasks;
using VisualMerchandiser.Models.Domain;
using VisualMerchandiser.Models.Domain.Repositories;

namespace VisualMerchandiser.Models.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(DbContext context)
        {
            Context = context;
        }

        public int Save()
        {
            return Context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public DbContext Context { get; }
    }
}