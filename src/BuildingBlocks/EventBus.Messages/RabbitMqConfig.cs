﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages
{
    public class RabbitMqConfig
    {
        public string Host = Environment.GetEnvironmentVariable("RABBITMQ_HOST");
        public int Port = int.Parse(Environment.GetEnvironmentVariable("RABBITMQ_PORT"));
        public string User = Environment.GetEnvironmentVariable("RABBITMQ_USER");
        public string Password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD");
        public int ConnectInterval { get; set; }
        public int ConnectRetries { get; set; }
    }

}
