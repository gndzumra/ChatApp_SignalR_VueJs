using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace ChatApp_API.Srv
{
    public class ChatHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Console.WriteLine("Hub Connected");
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("Hub Disconnected");
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string user, string message)
        {
            Console.WriteLine("Message received");
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
