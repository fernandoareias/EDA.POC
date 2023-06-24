using EDA.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDA.Post.Query.Domain.Events
{
    public class CommentUpdatedEvent : BaseEvent
    {
        public CommentUpdatedEvent() : base(nameof(BaseEvent))
        {
        }

        public Guid CommentId { get; set; }
        public string Comment { get; set; }
        public string Username { get; set; }
        public DateTime CommentDate { get; set; }
    }
}
