using System.ComponentModel.DataAnnotations.Schema;

namespace VisualMerchandiser.Models.Domain
{
    /// <summary>
    /// https://www.martinfowler.com/eaaCatalog/layerSupertype.html
    /// </summary>
    public class EntityBase
    {
        public int Id { get; set; }
        [NotMapped]
        public State State { get; set; }
    }
}