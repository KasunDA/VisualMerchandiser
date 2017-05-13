using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VisualMerchandiser.Models.Domain.Repositories;
using VisualMerchandiser.Models.Infrastructure.CrossCutting;

namespace VisualMerchandiser.Models.Domain
{
    public class Visit : EntityBase, IAggregate
    {
        public Visit()
        {
            CreatedDate = DateTime.UtcNow;
            Comments = new Collection<VisitComment>();
            Sections = new Collection<Section>();
        }
        public DateTime CreatedDate { get; set; }
        public DateTime ScheduledDate { get; set; }
        public DateTime? EndingDate { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int ShopId { get; set; }
        public virtual Shop Shop { get; set; }

        public virtual ICollection<VisitComment> Comments { get; set; }
        public virtual ICollection<Section> Sections { get; set; }

        public void Comment(User user, string text)
        {
            Comments.Add(VisitComment.Create(user, text));
        }

        public void Reply(User user, VisitComment comment, string text)
        {
            Comments.Add(VisitComment.Create(user, comment, text));
        }

        public void Comment(User user, Photo photo, string text)
        {
            photo.Comments.Add(new PhotoComment()
            {
                User = user,
                Text = text
            });
        }

        public void Reply(User user, Photo photo, PhotoComment comment, string text)
        {
            photo.Comments.Add(new PhotoComment()
            {
                User = user,
                Parent = comment,
                Text = text
            });
        }

        public void CreateSection(string name)
        {
            Sections.Add(new Section()
            {
                Name = name
            });
        }

        public void AddPhoto(Section section, byte[] content, PhotoMoment moment)
        {
            section.Photos.Add(new Photo()
            {
                PhotoContent = new PhotoContent()
                {
                    Content = content
                },
                Moment = moment
            });
        }

        public void RemovePhoto(Photo photo)
        {
            var section = photo.Section;
            section.Photos.Remove(photo);
            if (!section.Photos.Any())
            {
                section.Visit.Sections.Remove(section);
            }
        }

        public void RemoveSection(Section section)
        {
            section.Visit.Sections.Remove(section);
        }

        public void Reschedule(DateTime date)
        {
            if (date.Date < DateTime.UtcNow.Date)
            {
                throw new VisualMerchandiserException($"{nameof(date)} has to be greater than today");
            }
            ScheduledDate = date;
        }

        public void End()
        {
            EndingDate = DateTime.UtcNow;
        }
    }
}