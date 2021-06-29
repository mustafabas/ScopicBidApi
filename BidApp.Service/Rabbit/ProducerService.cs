using Microsoft.Extensions.Caching.Memory;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace BidApp.Service.Rabbit
{
    public class ProducerService : IProducerService
    {
        private int _messageCount = 1;
        private readonly IMemoryCache _memoryCache;

        public ProducerService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public bool PushMessageToQ()
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost:1572" };
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queue: "counter",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                        var message = $"Message {_messageCount++}";

                        Dictionary<string, int> messages = null;
                        _memoryCache.TryGetValue<Dictionary<string, int>>("messages", out messages);
                        if (messages == null) messages = new Dictionary<string, int>();
                        messages.Add(message, _messageCount);
                        _memoryCache.Set<Dictionary<string, int>>("messages", messages);

                        var messageBody = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(exchange: "counter", routingKey: "counter", body: messageBody, basicProperties: null);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message} | {ex.StackTrace}");
                return false;
            }
        }
    }
}
