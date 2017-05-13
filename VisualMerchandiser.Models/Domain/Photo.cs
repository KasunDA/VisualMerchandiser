using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using VisualMerchandiser.Models.Domain.Repositories;

namespace VisualMerchandiser.Models.Domain
{
    public class Photo : EntityBase
    {
        public Photo()
        {
            CreatedDate = DateTime.UtcNow;
            Comments = new Collection<PhotoComment>();
        }
        public DateTime CreatedDate { get; set; }
        public virtual PhotoContent PhotoContent { get; set; }
        public int SectionId { get; set; }
        public virtual Section Section { get; set; }
        public virtual ICollection<PhotoComment> Comments { get; set; }
        public PhotoMoment Moment { get; set; }
    }
}