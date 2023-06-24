using EDA.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDA.Core.Domain
{
    public abstract class AggregateRoot
    {
        public Guid Id { get; set; } 

        private List<BaseEvent> _events = new List<BaseEvent>();
        public IReadOnlyList<BaseEvent> Events => _events;

        public int Version { get; set; } = -1;

        public IEnumerable<BaseEvent> GetUncommittedEvents()
            => _events;

        public void MarkEventsAsCommitted()
            => _events.Clear();

        private void ApplyChange(BaseEvent @event, bool isNew)
        {
            var method = this.GetType().GetMethod("Apply", new Type[] { @event.GetType() });

            if (method == null)
                throw new ArgumentNullException(nameof(method), $"The Apply method was not found in the aggregate for {@event.GetType().Name}!");

            method.Invoke(this, new object[] { @event });

            if(isNew)
                _events.Add(@event);    
        }

        protected void RaiseEvent(BaseEvent @event)
        {
            ApplyChange(@event, true);
        }

        public void ReplayEvents(IEnumerable<BaseEvent> events)
        {
            foreach(var e  in events) 
                ApplyChange(e, false);
        }
    }
}
