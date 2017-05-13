using System.Data.Entity;
using VisualMerchandiser.Models.Domain;
using VisualMerchandiser.Models.Domain.Repositories;

namespace VisualMerchandiser.Models.Infrastructure.Persistence
{
    public static class DbContextExtensions
    {
        public static void ApplyStateChanges(this DbContext context)
        {
            foreach (var entry in context.ChangeTracker.Entries<EntityBase>())
            {
                entry.State = entry.Entity.State.ConvertToEntityState();
            }
        }
    }
}