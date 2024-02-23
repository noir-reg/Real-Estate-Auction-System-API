using BusinessObjects.Dtos.Request;
using BusinessObjects.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using NuGet.Protocol.Plugins;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Real_Estate_Auction_System_API.Hubs
{
   [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatHub : Hub, IChatHub
    {
        private static readonly Dictionary<string, string> userConnectionMap = new Dictionary<string, string>();
        private readonly IHubContext<ChatHub> _hubContext;
        public ChatHub(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public override async Task OnConnectedAsync()
        {
            string userId = Context.UserIdentifier;

            if (!userConnectionMap.ContainsKey(userId))
            {
                userConnectionMap.Add(userId, Context.ConnectionId);
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string userId = Context.UserIdentifier;

            if (userConnectionMap.ContainsKey(userId))
            {
                userConnectionMap.Remove(userId);
            }

            await base.OnDisconnectedAsync(exception);
        }
        public async Task SendPublicMessage(SendChatMessageDto sendChatMessage)
        {

            await _hubContext.Clients.All.SendAsync("SendPublicMessage", sendChatMessage.UserName, sendChatMessage.Message);
        }
        public async Task SendToGroup(SendToGroupDto sendToGroup)
        {
            await _hubContext.Clients.Group(sendToGroup.GroupId).SendAsync("SendToGroup", new { senderName = sendToGroup.SenderName }, sendToGroup.Message);
        }
        public async Task JoinToGroup(JoinToGroupDto joinToGroup)
        {
            userConnectionMap.TryGetValue(joinToGroup.UserId, out var connectionId);
            if (string.IsNullOrEmpty(connectionId))
            {
                throw new("Unauthorized");
            }
            await Groups.AddToGroupAsync(connectionId, joinToGroup.GroupId);
            await _hubContext.Clients.Group(joinToGroup.GroupId).SendAsync("SendToGroup", joinToGroup.UserId, $"{joinToGroup.UserId} has joined");

        }
        public async Task SendToUser(SendToUserDto sendToUser)
        {

            if (userConnectionMap.TryGetValue(sendToUser.ReceiverId, out var connectionId))
            {
                await _hubContext.Clients.Client(connectionId).SendAsync("SendToUser", sendToUser.Sender, sendToUser.Message);
            }
            else
            {
                throw new("ReceiverId not found");
            }
        }


    }
}