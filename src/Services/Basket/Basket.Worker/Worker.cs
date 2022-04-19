using Basket.Worker.Data.Context;
using Basket.Worker.Data.Context.Interfaces;
using Basket.Worker.Data.Messages;
using Basket.Worker.RabbitMQ;
using Basket.Worker.Services;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Basket.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly BasketService _basketService;
        private readonly RabbitService _rabbitMq;
        private readonly IBasketContext _context;

        public Worker(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Worker>();
            _context = new BasketContext();
            _basketService = new BasketService(_context, loggerFactory);
            _rabbitMq = new RabbitService();
            _logger.LogInformation("Initialization completed");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _rabbitMq.StartConnection("products");
            _rabbitMq.Consume("products", ProcessItemMessage);

            _logger.LogInformation("Started");

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(2000, stoppingToken);
            }
        }

        private void ProcessItemMessage(string json)
        {
            _logger.LogInformation("From RabiitMQ (queue = 'products') recieved: " + json);

            var message = JsonSerializer.Deserialize<ProductMessage>(json);

            if (message == null)
                _logger.LogError("Message error. Message is null");
            else if (message.Item == null)
                _logger.LogError("Message error. Message is empty, data is null: " + message.Action);
            else if (message.Action == "UpdateItem")
                _basketService.UpdateProductInBaskets(message.Item);
            else if (message.Action == "DeleteItem")
                _basketService.DeleteProductInBaskets(message.Item);
            else
                _logger.LogError("Message error. Unknown action in item message: " + message.Action);
        }

    }
}
