using System;

namespace VisualMerchandiser.Models.Domain
{
    public class VisitComment : Comment<VisitComment>
    {
        public int VisitId { get; set; }
        public virtual Visit Visit { get; set; }

        protected VisitComment()
        {

        }

        public static VisitComment Create(User user, string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentNullException(nameof(text));
            }
            return new VisitComment()
            {
                User = user,
                Text = text
            };
        }

        public static VisitComment Create(User user, VisitComment parent, string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentNullException(nameof(text));
            }
            return new VisitComment()
            {
                User = user,
                Parent = parent,
                Text = text
            };
        }
    }
}