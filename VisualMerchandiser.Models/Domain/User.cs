using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.AspNet.Identity.EntityFramework;

namespace VisualMerchandiser.Models.Domain
{
    public class User : IdentityUser<int, UserLogin, UserRole, UserClaim>, IAggregate
    {
        public User()
        {
            Visits = new Collection<Visit>();
            VisitComments = new Collection<VisitComment>();
            PhotoComments = new Collection<PhotoComment>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int? ParentUserId { get; set; }
        public virtual User ParentUser { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
        public ICollection<VisitComment> VisitComments { get; set; }
        public ICollection<PhotoComment> PhotoComments { get; set; }

        public void ScheduleVisit(Shop shop, DateTime date)
        {
            Visits.Add(new Visit
            {
                Shop = shop,
                ScheduledDate = date
            });
        }
    }
}