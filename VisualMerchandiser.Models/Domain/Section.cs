using System.Collections.Generic;
using System.Collections.ObjectModel;
using VisualMerchandiser.Models.Domain.Repositories;

namespace VisualMerchandiser.Models.Domain
{
    public class Section : EntityBase
    {
        public Section()
        {
            Photos = new Collection<Photo>();
        }

        public string Name { get; set; }
        public int VisitId { get; set; }
        public virtual Visit Visit { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}