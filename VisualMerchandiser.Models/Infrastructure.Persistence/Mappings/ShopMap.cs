using System.Data.Entity.ModelConfiguration;
using VisualMerchandiser.Models.Domain;

namespace VisualMerchandiser.Models.Infrastructure.Persistence.Mappings
{
    class ShopMap : EntityTypeConfiguration<Shop>
    {
        public ShopMap()
        {
            Property(shop => shop.Name).IsRequired();
        }
    }
}
