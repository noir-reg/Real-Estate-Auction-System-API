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
   public async Task<ListResponseBaseDto<GetRealEstatesResponseDto>> GetRealEstates()
   {
       throw new NotImplementedException();
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