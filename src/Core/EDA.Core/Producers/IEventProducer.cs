using EDA.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDA.Core.Producers
{
    public interface IEventProducer
    {
        Task ProducerAsync<T>(string topic, T @event) where T : BaseEvent;
    }
}
