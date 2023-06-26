using EDA.Core.Domain;
using EDA.Core.Handlers;
using EDA.Core.Infraestructure;
using EDA.Post.Cmd.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDA.Post.Cmd.Infraestructure.Handlers
{
    public class EventSourcingHandler : IEventSourcingHandler<Domain.Aggregates.PostAggregate>
    {
        private readonly IEventStore _eventStore;

        public EventSourcingHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<Domain.Aggregates.PostAggregate> GetByIdAsync(Guid aggregateId)
        {
            var aggregate = new Domain.Aggregates.PostAggregate();
            var events = await _eventStore.GetEventsAsync(aggregateId);

            if (events == null || !events.Any())
                return aggregate;

            aggregate.ReplayEvents(events);

            aggregate.Version = events.Select(x => x.Version).Max();

            return aggregate;

        }

        public async Task SaveAsync(AggregateRoot aggregateRoot)
        {
            await _eventStore.SaveEventsAsync(aggregateRoot.Id, aggregateRoot.GetUncommittedEvents(), aggregateRoot.Version);
            aggregateRoot.MarkEventsAsCommitted();
        }
    }
}
