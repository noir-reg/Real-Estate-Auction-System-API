﻿using BusinessObjects.Constants;
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
    private readonly ILegalDocumentService _legalDocumentService;
    private readonly IAuctionMediaService _auctionMediaService;

    public AuctionController(IAuctionService auctionService, ILegalDocumentService legalDocumentService, IAuctionMediaService auctionMediaService)
    {
        _auctionService = auctionService;
        _legalDocumentService = legalDocumentService;
        _auctionMediaService = auctionMediaService;
    }

    
    [HttpPost()]
    public async Task<ActionResult<ResultResponse<CreateAuctionResponseDto>>> CreateAuction([FromBody] CreateAuctionRequestDto request)
    {
        var response = await _auctionService.CreateAuction(request);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResultResponse<AuctionPostDetailResponseDto>>> GetAuctionById([FromRoute] Guid id)
    {
        var response = await _auctionService.GetAuctionById(id);
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<ListResponseBaseDto<AuctionPostListResponseDto>>> GetAuctions([FromQuery] AuctionQuery request)
    {
       ListResponseBaseDto<AuctionPostListResponseDto> response = await _auctionService.GetAuctions(request);
       return Ok(response);
    }
    
    [HttpPost("{auctionId}/legal-documents")]
    public async Task<ActionResult<ResultResponse<UploadDocumentsResponseDto>>> UploadDocuments(
        [FromRoute] Guid auctionId, IFormFile file)
    {
        var fileTypeChecker = new FileTypeChecker();
        if (!fileTypeChecker.IsValidFileType(file.ContentType))
        {
            return BadRequest();
        }

        if (file.Length > FileChecker.FILE_SIZE_LIMIT)
        {
            return BadRequest();
        }

        var result = await _legalDocumentService.UploadLegalDocuments(auctionId, file);
        return Ok(result);
    }

    [HttpPost("{auctionId}/auction-medias")]
    public async Task<ActionResult<ResultResponse<UploadMediaResponseDto>>> UploadMedia(
        [FromRoute] Guid auctionId, IFormFile file)
    {
        var fileTypeChecker = new FileTypeChecker();
        if (!fileTypeChecker.IsValidFileType(file.ContentType))
        {
            return BadRequest();
        }
        
        if (file.Length > FileChecker.FILE_SIZE_LIMIT)
        {
            return BadRequest();
        }
        
        ResultResponse<UploadMediaResponseDto> result = await _auctionMediaService.UploadMedia(auctionId, file);
        return Ok(result);

    }

    [HttpGet("{auctionId}/legal-documents")]
    public async Task<ActionResult<ListResponseBaseDto<GetLegalDocumentsResponseDto>>> GetLegalDocuments(
        [FromRoute] Guid auctionId,
        [FromQuery] LegalDocumentQuery query)
    {
        var result = await _legalDocumentService.GetLegalDocuments(auctionId: auctionId, query: query);

        return Ok(result);
    }

    [HttpGet("{auctionId}/auction-medias")]
    public async Task<ActionResult<ListResponseBaseDto<GetAuctionMediasResponseDto>>> GetAuctionMedias(
        [FromRoute] Guid auctionId,[FromQuery] AuctionMediaQuery query)
    {
       var result = await _auctionMediaService.GetMedia(auctionId,query);
       return Ok(result);
    }
}

