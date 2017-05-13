using System.Data.Entity.ModelConfiguration;
using VisualMerchandiser.Models.Domain;

namespace VisualMerchandiser.Models.Infrastructure.Persistence.Mappings
{
    class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            HasMany(user => user.VisitComments).WithRequired(visitComment => visitComment.User).WillCascadeOnDelete(false);
            HasMany(user => user.PhotoComments).WithRequired(photoComment => photoComment.User).WillCascadeOnDelete(false);
        }
    }
}