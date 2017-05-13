using System.Data.Entity.ModelConfiguration;
using VisualMerchandiser.Models.Domain;

namespace VisualMerchandiser.Models.Infrastructure.Persistence.Mappings
{
    class PhotoCommentMap : EntityTypeConfiguration<PhotoComment>
    {
        public PhotoCommentMap()
        {
            Property(photoComment => photoComment.Text).IsRequired();
        }
    }
}