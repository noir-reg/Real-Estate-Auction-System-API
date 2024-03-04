using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using Microsoft.AspNetCore.Http;

namespace Services;

public interface ILegalDocumentService
{
    Task<ResultResponse<UploadDocumentsResponseDto>> UploadLegalDocuments(Guid realEstateId, IFormFile file);
    Task<ListResponseBaseDto<GetLegalDocumentsResponseDto>> GetLegalDocuments(LegalDocumentQuery query);
    Task<ListResponseBaseDto<GetLegalDocumentsResponseDto>> GetLegalDocuments(Guid realEstateId,LegalDocumentQuery query);
}