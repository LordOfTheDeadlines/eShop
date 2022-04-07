using Catalog.Worker.Data.Context;
using Catalog.Worker.Data.Context.Interfaces;
using Catalog.Worker.Data.Messages;
using Catalog.Worker.RabbitMQ;
using Catalog.Worker.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly CatalogService _catalogService;
        private readonly RabbitService _rabbitMq;
        private readonly ICatalogContext _context;

        public Worker(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Worker>();
            _context = new CatalogContext();
            _catalogService = new CatalogService(_context, loggerFactory);
            _rabbitMq = new RabbitService();
            _logger.LogInformation("Initialization completed");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _rabbitMq.StartConnection("products", "categories");
            _rabbitMq.Consume("products", ProcessItemMessage);
            _rabbitMq.Consume("categories", ProcessCategoryMessage);

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
            else if (message.Action == "AddItem")
                _catalogService.AddProduct(message.Item);
            else if (message.Action == "UpdateItem")
                _catalogService.UpdateProduct(message.Item);
            else if (message.Action == "DeleteItem")
                _catalogService.DeleteProduct(message.Item);
            else
                _logger.LogError("Message error. Unknown action in item message: " + message.Action);
        }

        private void ProcessCategoryMessage(string json)
        {
            _logger.LogInformation("From RabiitMQ (queue = 'categories') recieved: " + json);

            var message = JsonSerializer.Deserialize<CategoryMessage>(json);

            if (message == null)
                _logger.LogError("Message error. Message is null");
            else if (message.Category == null)
                _logger.LogError("Message error. Message is empty, data is null: " + message.Action);
            else if (message.Action == "AddCategory")
                _catalogService.AddCategory(message.Category);
            else if (message.Action == "UpdateCategory")
                _catalogService.UpdateCategory(message.Category);
            else if (message.Action == "DeleteCategory")
                _catalogService.DeleteCategory(message.Category);
            else
                _logger.LogError("Message error. Unknown action in item message: " + message.Action);
        }

    }
}
