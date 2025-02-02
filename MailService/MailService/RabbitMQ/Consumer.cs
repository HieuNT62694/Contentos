﻿using MailService.Controllers;
using MailService.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MailService.Application.EmailSender;

namespace MailService.RabbitMQ
{
    public class Consumer : BackgroundService 
    {
        private IConnection connection;
        private IModel channel;
        private string queueName;

        public Consumer(string exch)
        {
            InitRabbitMQ(exch);

        }
        private void InitRabbitMQ(string exch)
        {
            var factory = new ConnectionFactory() { HostName = "35.240.195.137" }; ;

            connection = factory.CreateConnection();
            channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: exch, type: ExchangeType.Fanout);

            queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName,
                              exchange: exch,
                              routingKey: "");
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                var messageAccountDTO = JsonConvert.DeserializeObject<MessageAccountDTO>(message);
                var email = new EmailSender();
                await email.SendEmailAsync(messageAccountDTO.Email, "Welcome To Contento System", "Username: " + messageAccountDTO.FullName + ", Password: " + messageAccountDTO.Password);
            };
            channel.BasicConsume(queue: queueName,
                                 autoAck: false,
                                 consumer: consumer);
            return Task.CompletedTask;
        }
    }
    
}
