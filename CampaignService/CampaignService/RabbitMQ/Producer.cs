﻿using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignService.RabbitMQ
{
    public class Producer
    {
        public void PublishMessage(string message, string exch)
        {
            var factory = new ConnectionFactory() { HostName = "35.240.195.137" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: exch,
                                     routingKey: "",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }
        }
    }
}
