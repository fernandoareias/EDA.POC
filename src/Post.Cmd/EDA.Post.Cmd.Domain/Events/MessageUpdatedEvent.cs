using EDA.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDA.Post.Cmd.Domain.Events
{
    public class MessageUpdatedEvent : BaseEvent
    {
        public MessageUpdatedEvent() : base(nameof(MessageUpdatedEvent))
        {
            
        }

        public string Message { get; set; }
    }
}
