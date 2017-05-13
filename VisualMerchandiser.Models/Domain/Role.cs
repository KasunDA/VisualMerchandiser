using Microsoft.AspNet.Identity.EntityFramework;

namespace VisualMerchandiser.Models.Domain
{
    public class Role : IdentityRole<int, UserRole>
    {
        public Role()
        {
            
        }

        public Role(string name)
        {
            Name = name;
        }
    }
}