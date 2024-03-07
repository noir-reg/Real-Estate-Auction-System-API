using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Real_Estate_Auction_System_API.Controllers;

[ApiController]
[Route("api/real-estates")]
public class RealEstateController : ControllerBase
{
   private readonly IRealEstateService _realEstateService;
   private readonly ILegalDocumentService _legalDocumentService;

   public RealEstateController(IRealEstateService realEstateService, ILegalDocumentService legalDocumentService)
   {
       _realEstateService = realEstateService;
       _legalDocumentService = legalDocumentService;
   }

   [HttpGet]
   public async Task<ActionResult<ListResponseBaseDto<GetRealEstatesResponseDto>>> GetRealEstates([FromQuery] RealEstateQuery request)
   {
       var result = await _realEstateService.GetRealEstates(request);
       return Ok(result);
   }

   [HttpPost]
   public async Task<ActionResult<ResultResponse<CreateRealEstateResponseDto>>> CreateRealEstate(
       CreateRealEstateRequestDto request)
   {
      ResultResponse<CreateRealEstateResponseDto> result = await _realEstateService.CreateRealEstate(request);
      return Ok(result);
   }
   
   [HttpPut("{id}")]
   public async Task<ActionResult<ResultResponse<UpdateRealEstateResponseDto>>> UpdateRealEstate([FromRoute] Guid id, UpdateRealEstateRequestDto request)
   {
       ResultResponse<UpdateRealEstateResponseDto> result = await _realEstateService.UpdateRealEstate(id, request);

       if (result.Status == Status.NotFound)
       {
           return NotFound(result);
       }
       
       return Ok(result);
   }

   [HttpDelete("{id}")]
   public async Task<ActionResult<ResultResponse<DeleteRealEstateResponseDto>>> DeleteRealEstate([FromRoute] Guid id)
   {
      ResultResponse<DeleteRealEstateResponseDto> result = await _realEstateService.DeleteRealEstate(id);

      if (result.Status == Status.NotFound)
      {
          return NotFound(result);
      }
      
      return Ok(result);
   }

   [HttpGet("{id}")]
   public async Task<ResultResponse<GetSingleRealEstateResponseDto>> GetRealEstateById(int id)
   {
       throw new NotImplementedException();
   }

   [HttpPost("{realEstateId}/legal-documents")]
   public async Task<ActionResult<ResultResponse<UploadDocumentsResponseDto>>> UploadDocuments([FromRoute] Guid realEstateId, IFormFile file)
   {
        var result = await _legalDocumentService.UploadLegalDocuments(realEstateId,file);
        return Ok(result);
   }

   [HttpGet("{realEstateId}/legal-documents")]
   public async Task<ActionResult<ListResponseBaseDto<GetLegalDocumentsResponseDto>>> GetLegalDocuments([FromRoute] Guid realEstateId,
       [FromQuery] LegalDocumentQuery query)
   {
       var result = await _legalDocumentService.GetLegalDocuments(realEstateId: realEstateId, query: query);
       
       return  Ok(result);
   }
}