using EDA.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDA.Core.Domain
{
    public interface IEventStoreRepository
    {
        Task SaveAsync(EventModel @event);
        Task<List<EventModel>> FindByAggregateId(Guid aggregateId);
    }
}
