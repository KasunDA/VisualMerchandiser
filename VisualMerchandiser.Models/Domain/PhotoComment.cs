namespace VisualMerchandiser.Models.Domain
{
    public class PhotoComment : Comment<PhotoComment>
    {
        public int PhotoId { get; set; }
        public virtual Photo Photo { get; set; }
    }
}