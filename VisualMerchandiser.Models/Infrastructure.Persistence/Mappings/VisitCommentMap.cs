using System.Data.Entity.ModelConfiguration;
using VisualMerchandiser.Models.Domain;

namespace VisualMerchandiser.Models.Infrastructure.Persistence.Mappings
{
    class VisitCommentMap : EntityTypeConfiguration<VisitComment>
    {
        public VisitCommentMap()
        {
            Property(visitComment => visitComment.Text).IsRequired();
        }
    }
}