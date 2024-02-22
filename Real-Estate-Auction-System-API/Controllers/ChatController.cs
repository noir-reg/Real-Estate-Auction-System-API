using BusinessObjects.Dtos.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Real_Estate_Auction_System_API.Hubs;

namespace Real_Estate_Auction_System_API.Controllers
{
    [ApiController]
    [Route("api/chat")]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IChatHub _chatHub;
        public ChatController(IHubContext<ChatHub> hubContext, IChatHub chatHub)
        {
            _hubContext = hubContext;
            _chatHub = chatHub;
        }

        [HttpPost("send-to-user")]
        public async Task<IActionResult> SendToUser([FromBody] SendToUserDto sendToUserDto)
        {
            await _chatHub.SendToUser(sendToUserDto);
            return Ok("Message sent to user successfully");
        }

        [HttpPost("join-to-group")]
        public async Task<IActionResult> JoinToGroup([FromBody] JoinToGroupDto joinToGroupDto)
        {
            await _chatHub.JoinToGroup(joinToGroupDto);
            return Ok($"User {joinToGroupDto.UserId} joined group {joinToGroupDto.GroupId} successfully");
        }

        [HttpPost("send-to-group")]
        public async Task<IActionResult> SendToGroup([FromBody] SendToGroupDto sendToGroupDto)
        {
            await _chatHub.SendToGroup(sendToGroupDto);
            return Ok($"Message sent to group {sendToGroupDto.GroupId} successfully");
        }

        [HttpPost("send-public-message")]
        public async Task<IActionResult> SendPublicMessage([FromBody] SendChatMessageDto sendChatMessageDto)
        {
            await _chatHub.SendPublicMessage(sendChatMessageDto);
            return Ok("Public message sent successfully");
        }
    }
}
