using EDA.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDA.Post.Cmd.Domain.Events
{
    public class PosteLikedEvent : BaseEvent
    {
        public PosteLikedEvent() : base(nameof(PosteLikedEvent))
        {
        }
    }
}
