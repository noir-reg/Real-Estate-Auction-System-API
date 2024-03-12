using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Real_Estate_Auction_System_API.Controllers;

[ApiController]
[Route("api/auctions")]
public class AuctionController : ControllerBase
{
    private readonly IAuctionService _auctionService;

    public AuctionController(IAuctionService auctionService)
    {
        _auctionService = auctionService;
    }

    // [HttpGet]
    // public async Task<ActionResult<ListResponseDto<Auction>>> GetAll([FromBody] ListRequestDto<Auction> request)
    // {
    //     var response = await _auctionService.GetAuctions(request);
    //     return Ok(response);
    // }
    
    [HttpPost()]
    public async Task<ActionResult<ResultResponse<CreateAuctionResponseDto>>> CreateAuction([FromBody] CreateAuctionRequestDto request)
    {
        var response = await _auctionService.CreateAuction(request);
        return Ok(response);
    }
}