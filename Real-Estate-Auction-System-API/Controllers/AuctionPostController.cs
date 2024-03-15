using Microsoft.AspNetCore.Mvc;
using Services;

namespace Real_Estate_Auction_System_API.Controllers;

[ApiController]
[Route("api/auction-posts")]
public class AuctionPostController :ControllerBase
{
    private readonly IAuctionPostService _auctionPostService;
}