using BusinessObjects.Dtos.Request;

namespace Real_Estate_Auction_System_API.Hubs
{
    public interface IChatHub
    {
        public Task SendPublicMessage(SendChatMessageDto sendChatMessage);
        public Task SendToGroup(SendToGroupDto sendToGroup);
        public Task JoinToGroup(JoinToGroupDto joinToGroupDto);
        public Task SendToUser(SendToUserDto sendToUserDto);

    }
}
