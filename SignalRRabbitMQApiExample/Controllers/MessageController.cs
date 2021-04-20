using System;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using SignalRRabbitMQApiExample.Models;

namespace SignalRRabbitMQApiExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {

        [HttpPost]
        public IActionResult Post([FromForm]User model)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri("amps name");
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            channel.QueueDeclare("messagequeue",false,false,false);

            string serializeData = JsonSerializer.Serialize(model);

            byte[] data = Encoding.UTF8.GetBytes(serializeData);

            channel.BasicPublish("","messagequeue",body:data);

            return Ok();
        }
    }
}
