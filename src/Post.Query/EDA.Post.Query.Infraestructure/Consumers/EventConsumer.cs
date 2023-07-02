using Confluent.Kafka;
using EDA.Core.Consumers;
using EDA.Core.Events;
using EDA.Post.Query.Infraestructure.Consumers.Configs;
using EDA.Post.Query.Infraestructure.Converters;
using EDA.Post.Query.Infraestructure.Handlers;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EDA.Post.Query.Infraestructure.Consumers
{
    public class EventConsumer : IEventConsumer
    {
        private readonly Confluent.Kafka.ConsumerConfig _config;
        private readonly IEventHandler _eventHandler;

        public EventConsumer(IEventHandler eventHandler, IOptions<Confluent.Kafka.ConsumerConfig> option)
        {
            _eventHandler = eventHandler;
            _config = option.Value;
        }

        public void Consume(string topic)
        {
            using var consumer = new ConsumerBuilder<string, string>(_config)
                        .SetKeyDeserializer(Deserializers.Utf8)
                        .SetValueDeserializer(Deserializers.Utf8)
                        .Build();

            consumer.Subscribe(topic);

            while (true)
            {
                var consumerResult = consumer.Consume();

                if (consumerResult?.Message == null) continue;

                var options = new JsonSerializerOptions { Converters = { new EventJsonConverter() } };
                var @event = JsonSerializer.Deserialize<BaseEvent>(consumerResult.Message.Value, options);

                var handlerMethod = _eventHandler.GetType().GetMethod("On", new Type[] { @event.GetType() });

                if (handlerMethod == null)
                    throw new ArgumentNullException(nameof(handlerMethod), "Could not find event handler method!");

                handlerMethod.Invoke(_eventHandler, new object[] { @event });
                consumer.Commit(consumerResult);
            }
        }
    }
}
