using BidApp.Service.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BidApp.Service.Hubs
{
    public class BidHub : Hub, IBidHub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public async Task SendMqMessage(List<string> messages)
        {
            await Clients.All.SendAsync("ReceiveMq", messages);
        }
    }
}
