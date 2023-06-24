using EDA.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDA.Post.Query.Domain.Events
{
    public class CommentRemovedEvent : BaseEvent
    {
        public CommentRemovedEvent() : base(nameof(BaseEvent))
        {
        }

        public Guid CommentId { get; set; }
    }
}
