using Microsoft.AspNetCore.SignalR;

namespace Real_Estate_Auction_System_API.Hubs;

public class ChatHub : Hub
{
    public async Task SendMessage(string userName, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", userName, message);
    }
}