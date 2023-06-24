using EDA.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDA.Post.Cmd.Domain.Events
{
    public class PostCreatedEvent : BaseEvent
    {
        public PostCreatedEvent() : base(nameof(PostCreatedEvent))
        {
        }

        public string Author { get; set; }
        public string Message { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
