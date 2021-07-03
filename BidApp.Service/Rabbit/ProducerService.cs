using BidApp.Service.Hubs;
using Microsoft.Extensions.Caching.Memory;
using Nancy.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace BidApp.Service.Rabbit
{
    public class ProducerService : IProducerService
    {
 
        public bool PushMessageToQ(MessageModel model)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        /*channel.QueueDeclare(queue: "bidMessage",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);


              
                        var messageBody = Encoding.UTF8.GetBytes(json);

                        channel.BasicPublish(exchange: "bidMessage", routingKey: "bidMessage", body: messageBody, basicProperties: null);*/


                        channel.QueueDeclare(queue: "bid.app.amounts",
                           durable: true,
                           exclusive: false,
                           autoDelete: false,
                           arguments: null);

                        var json = new JavaScriptSerializer().Serialize(model);
                        var body = Encoding.UTF8.GetBytes(json);

                        channel.BasicPublish(exchange: "",
                                             routingKey: "bid.app.amounts",
                                             basicProperties: null,
                                             body: body);
                        Console.WriteLine(" [x] Sent {0}", json);
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
