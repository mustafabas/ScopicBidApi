using BidApp.Entities;
using BidApp.Service.Hubs;
using BidApp.Service.Products;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nancy.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BidApp.WebApi.HostedServices
{
    public class ConsumeRabbitMqService : BackgroundService
    {
        readonly ILogger _logger;
        IConnection _connection;
        IModel _channel;
        private readonly IServiceProvider serviceProvider;

        readonly IProductBidService _productBidService;



        public ConsumeRabbitMqService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            InitRabbitMQ();

        }

        private void InitRabbitMQ()
        {
          var factory = new ConnectionFactory { HostName = "localhost" };

            // create connection  
            _connection = factory.CreateConnection();

            // create channel  
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "bid.app.amounts",
                                                    durable: true,
                                                    exclusive: false,
                                                    autoDelete: false,
                                                    arguments: null);

            _channel.BasicQos(0, 1, false);

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                // received message  
                var body = ea.Body.ToArray();

                var content = System.Text.Encoding.UTF8.GetString(body);

                // handle the received message  
                HandleMessage(content);
                _channel.BasicAck(ea.DeliveryTag, false);
            };

            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _channel.BasicConsume("bid.app.amounts", false, consumer);
            return Task.CompletedTask;
        }

        private async void HandleMessage(string content)
        {

            MessageModel messageModel = new JavaScriptSerializer().Deserialize<MessageModel>(content);
            ProductBidEntity productBidEntity = new ProductBidEntity();
            productBidEntity.Offer = messageModel.Price;
            productBidEntity.ProductId = messageModel.ProductId;
            productBidEntity.RecordDate = DateTime.Now;
            productBidEntity.UserId = messageModel.UserId;
            using (var scope = serviceProvider.CreateScope())
            {
                var _productBidService = scope.ServiceProvider.GetService<IProductBidService>();
              await _productBidService.InsertProductBid(productBidEntity);

                var _productService = scope.ServiceProvider.GetService<IProductService>();
                var product = _productService.GetProductById(messageModel.ProductId);
                product.CurrentPrice = messageModel.Price;
                _productService.UpdateProduct(product);
            }


     
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) { }
        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { }
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e) { }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
