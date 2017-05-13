using System.Data.Entity.ModelConfiguration;
using VisualMerchandiser.Models.Domain;

namespace VisualMerchandiser.Models.Infrastructure.Persistence.Mappings
{
    class PhotoMap : EntityTypeConfiguration<Photo>
    {
        public PhotoMap()
        {
            //Entity splitting
            HasRequired(photo => photo.PhotoContent)
                .WithRequiredPrincipal(photoContent => photoContent.Photo)
                .WillCascadeOnDelete(true);
        }
    }
}