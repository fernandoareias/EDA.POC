using EDA.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDA.Core.Events
{
    public abstract class BaseEvent : Message
    {
        protected BaseEvent(string type)
        {
            Type = type;    
        }
        public int Version { get; set; }
        public string Type { get; set; }
    }
}
