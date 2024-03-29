﻿using BusinessObjects.Constants;
using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using BusinessObjects.Enums;
using Microsoft.AspNetCore.Authorization;
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

    public AuctionController(IAuctionService auctionService, ILegalDocumentService legalDocumentService,
        IAuctionMediaService auctionMediaService)
    {
        _auctionService = auctionService;
        _legalDocumentService = legalDocumentService;
        _auctionMediaService = auctionMediaService;
    }


    [HttpPost()]
    [AllowAnonymous]
    public async Task<ActionResult<ResultResponse<CreateAuctionResponseDto>>> CreateAuction(
        [FromBody] CreateAuctionRequestDto request)
    {
        var response = await _auctionService.CreateAuction(request);
        return Ok(response);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<ResultResponse<AuctionPostDetailResponseDto>>> GetAuctionById([FromRoute] Guid id)
    {
        var response = await _auctionService.GetAuctionById(id);
        return Ok(response);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<ListResponseBaseDto<AuctionPostListResponseDto>>> GetAuctions(
        [FromQuery] AuctionQuery request)
    {
        ListResponseBaseDto<AuctionPostListResponseDto> response = await _auctionService.GetAuctions(request);
        return Ok(response);
    }

    [HttpPost("{auctionId}/legal-documents")]
    [Consumes(FileTypes.PNG, FileTypes.JPEG, FileTypes.PDF, FileTypes.XLSX, FileTypes.XLS, FileTypes.DOC,
        FileTypes.DOCX)]
    [RequestFormLimits(ValueLengthLimit = (int)FileChecker.FILE_SIZE_LIMIT,
        MultipartBodyLengthLimit = FileChecker.FILE_SIZE_LIMIT)]
    [AllowAnonymous]
    public async Task<ActionResult<ResultResponse<UploadDocumentsResponseDto>>> UploadDocuments(
        [FromRoute] Guid auctionId, IFormFile file)
    {
        var result = await _legalDocumentService.UploadLegalDocuments(auctionId, file);
        return Ok(result);
    }

    [HttpPost("{auctionId}/auction-medias")]
    [Consumes(FileTypes.PNG, FileTypes.JPEG)]
    [RequestFormLimits(ValueLengthLimit = (int)FileChecker.FILE_SIZE_LIMIT,
        MultipartBodyLengthLimit = FileChecker.FILE_SIZE_LIMIT)]
    [AllowAnonymous]
    public async Task<ActionResult<ResultResponse<UploadMediaResponseDto>>> UploadMedia(
        [FromRoute] Guid auctionId, IFormFile file)
    {
        ResultResponse<UploadMediaResponseDto> result = await _auctionMediaService.UploadMedia(auctionId, file);
        return Ok(result);
    }

    [HttpGet("{auctionId}/legal-documents")]
    [AllowAnonymous]
    public async Task<ActionResult<ListResponseBaseDto<GetLegalDocumentsResponseDto>>> GetLegalDocuments(
        [FromRoute] Guid auctionId,
        [FromQuery] LegalDocumentQuery query)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var result = await _legalDocumentService.GetLegalDocuments(auctionId: auctionId, query: query);

        return Ok(result);
    }

    [HttpGet("{auctionId}/auction-medias")]
    [AllowAnonymous]
    public async Task<ActionResult<ListResponseBaseDto<GetAuctionMediasResponseDto>>> GetAuctionMedias(
        [FromRoute] Guid auctionId, [FromQuery] AuctionMediaQuery query)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var result = await _auctionMediaService.GetMedia(auctionId, query);
        return Ok(result);
    }

    [HttpPut("{auctionId}")]
    [AllowAnonymous]
    public async Task<ActionResult<ResultResponse<UpdateAuctionResponseDto>>> UpdateAuction(
        [FromRoute] Guid auctionId, [FromBody] UpdateAuctionRequestDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        ResultResponse<UpdateAuctionResponseDto> result = await _auctionService.UpdateAuction(auctionId, request);
        return Ok(result);
    }

    [HttpDelete("{auctionId}")]
    [AllowAnonymous]
    public async Task<ActionResult<ResultResponse<DeleteAuctionResponseDto>>> DeleteAuction(
        [FromRoute] Guid auctionId)
    {
        ResultResponse<DeleteAuctionResponseDto> result = await _auctionService.DeleteAuction(auctionId);
        return Ok(result);
    }
}