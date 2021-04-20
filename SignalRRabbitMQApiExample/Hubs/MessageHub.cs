using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalRRabbitMQApiExample.Hubs
{
    public class MessageHub:Hub
    {
        public async Task SendMessageAsync(string message,string connId)
        {

            await Clients.Client(connId).SendAsync("receiveMessage", message);
        }


        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("connId", Context.ConnectionId);
        }
    }
}