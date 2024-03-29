﻿using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Real_Estate_Auction_System_API.Controllers;

[ApiController]
[Route("api/bids")]
public class BidController : ControllerBase
{
    private readonly IBidService _bidService;
    

    public BidController(IBidService bidService)
    {
        _bidService = bidService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<ListResponseBaseDto<GetBidResponseDto>>> GetBids([FromQuery] BaseQueryDto request)
    {
      
        var result = await _bidService.GetBids(request);
        return Ok(result);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<ResultResponse<CreateBidResponseDto>>> CreateBid([FromBody]
        CreateBidRequestDto request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState); 
        
        ResultResponse<CreateBidResponseDto> result = await _bidService.CreateBid(request);
        return Ok(result);
    }
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<ResultResponse<GetBidResponseDto>>> GetBidById([FromRoute]Guid id)
    {
        ResultResponse<GetBidResponseDto> result = await _bidService.GetBidById(id);
        if (result.Status == Status.NotFound)
        {
            return NotFound(result);
        }
        return Ok(result);
    }
}