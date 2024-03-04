using BusinessObjects.Dtos.Response;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Services;

namespace Real_Estate_Auction_System_API.Controllers;

[ApiController]
[Route("api/legal-documents")]
public class LegalDocumentController : ControllerBase
{
   private readonly ILegalDocumentService _legalDocumentService;

   public LegalDocumentController(ILegalDocumentService legalDocumentService)
   {
      _legalDocumentService = legalDocumentService;
   }

  
   
}