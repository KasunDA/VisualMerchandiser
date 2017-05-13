using System.Data.Entity.ModelConfiguration;
using VisualMerchandiser.Models.Domain;

namespace VisualMerchandiser.Models.Infrastructure.Persistence.Mappings
{
    class SectionMap : EntityTypeConfiguration<Section>
    {
        public SectionMap()
        {
            Property(section => section.Name).IsRequired();
        }
    }
}