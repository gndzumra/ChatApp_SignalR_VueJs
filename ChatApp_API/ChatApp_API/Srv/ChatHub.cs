using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace ChatApp_API.Srv
{
      public class ChatHub : Hub<IChatHub>
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


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.ReceiveMessage(user, message);
            Console.WriteLine("Message received");
        }
    }
}
