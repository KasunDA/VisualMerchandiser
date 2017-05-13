using System;
using System.Collections.Generic;
using VisualMerchandiser.Models.Domain.Repositories;

namespace VisualMerchandiser.Models.Domain
{
    public abstract class Comment<T> : EntityBase
    {
        protected Comment()
        {
            CreatedDate = DateTime.UtcNow;
        }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string Text { get; set; }
        public int? ParentId { get; set; }
        public virtual T Parent { get; set; }
        public ICollection<T> Children { get; set; }
    }
}