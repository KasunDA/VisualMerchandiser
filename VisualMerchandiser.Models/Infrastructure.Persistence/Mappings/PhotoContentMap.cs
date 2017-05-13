using System.Data.Entity.ModelConfiguration;
using VisualMerchandiser.Models.Domain;

namespace VisualMerchandiser.Models.Infrastructure.Persistence.Mappings
{
    class PhotoContentMap : EntityTypeConfiguration<PhotoContent>
    {
        public PhotoContentMap()
        {
            Property(photoContent => photoContent.Content).IsRequired();
        }
    }
}