﻿using Microsoft.AspNetCore.SignalR;

namespace Kaïbay.Hubs
{
    public class EbayHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
