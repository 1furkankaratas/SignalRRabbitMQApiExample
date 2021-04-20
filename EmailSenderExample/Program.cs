using System;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.SignalR.Client;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EmailSenderExample
{
    class Program
    {
        static void Main(string[] args)
        {
            HubConnection connectionHub = new HubConnectionBuilder().WithUrl("http://localhost:5000/messagehub").Build();
            connectionHub.StartAsync();
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri("amps name");
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            channel.QueueDeclare("messagequeue", false, false, false);

            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume("messagequeue", true, consumer);

            consumer.Received += async (s, e) =>
            {

                string serializeData = Encoding.UTF8.GetString(e.Body.Span);
                User user = JsonSerializer.Deserialize<User>(serializeData);

                EmailSender.Send(user.Email,user.Message);
                Console.WriteLine($"{user.Email} mail gönderdi");
                await connectionHub.InvokeAsync("SendMessageAsync", $"{user.Email} mail gönderdi",user.ConnectionId);

            };

            Console.Read();
        }
    }
}
