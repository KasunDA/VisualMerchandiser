using VisualMerchandiser.Models.Domain.Repositories;

namespace VisualMerchandiser.Models.Domain
{
    public class PhotoContent:EntityBase
    {
        public virtual Photo Photo { get; set; }
        public byte[] Content { get; set; } 
        public byte[] ThumbnailContent { get; set; }
    }
}