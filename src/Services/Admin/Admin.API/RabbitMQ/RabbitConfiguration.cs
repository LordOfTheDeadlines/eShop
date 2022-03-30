using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.API.RabbitMQ
{
    public class RabbitConfiguration
    {
        public string Host = Environment.GetEnvironmentVariable("RABBITMQ_HOST");
        public string User = Environment.GetEnvironmentVariable("RABBITMQ_USER");
        public string Password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD");
        public int Port = int.Parse(Environment.GetEnvironmentVariable("RABBITMQ_PORT"));
        public int ConnectInterval { get; set; }
        public int ConnectRetries { get; set; }
    }
}
