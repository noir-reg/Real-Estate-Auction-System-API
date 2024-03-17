using System.Linq.Expressions;
using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using BusinessObjects.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Services;

public class LegalDocumentService : ILegalDocumentService
{
    private readonly ILegalDocumentRepository _legalDocumentRepository;
    private readonly IFirebaseStorageService _firebaseStorageService;

    public LegalDocumentService(ILegalDocumentRepository legalDocumentRepository,
        IFirebaseStorageService firebaseStorageService)
    {
        _firebaseStorageService = firebaseStorageService;
        _legalDocumentRepository = legalDocumentRepository;
    }

    public async Task<ResultResponse<UploadDocumentsResponseDto>> UploadLegalDocuments(Guid auctionId,
        IFormFile file)
    {
        try
        {
            var filePath = Path.GetTempFileName();
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var objectName = $"{Guid.NewGuid()}_{file.FileName}";
            var gcsUri =
                await _firebaseStorageService.UploadFileAsync(filePath, objectName, contentType: file.ContentType);

            File.Delete(filePath);

            var legalDocument = new LegalDocument()
            {
                AuctionId = auctionId,
                FileName = objectName,
                DocumentType = file.ContentType,
                DocumentUrl = gcsUri,
                Description = file.ContentDisposition
            };

            var data = await _legalDocumentRepository.Add(legalDocument);


            var result = new ResultResponse<UploadDocumentsResponseDto>()
            {
                Status = Status.Ok,
                Messages = new[] { "Legal Document uploaded successfully" },
                Data = new UploadDocumentsResponseDto()
                {
                    LegalDocumentId = data.DocumentId,
                    DocumentUrl = data.DocumentUrl,
                    DocumentType = data.DocumentType,
                    Title = data.FileName,
                    Description = data.Description,
                    AuctionId = data.AuctionId
                },
                IsSuccess = true
            };

            return result;
        }
        catch (Exception e)
        {
            return ErrorResponse.CreateErrorResponse<UploadDocumentsResponseDto>(e);
        }
    }

    public async Task<ListResponseBaseDto<GetLegalDocumentsResponseDto>> GetLegalDocuments(Guid auctionId,LegalDocumentQuery query)
    {
        try
        {
            var queryable = _legalDocumentRepository.GetLegalDocumentQuery();

            Expression<Func<LegalDocument, bool>> expression = x => x.FileName.Contains(query.FileName);


            queryable = queryable.Where(expression);

            queryable = query.SortBy switch
            {
                LegalDocumentSortBy.FileName => query.OrderDirection == OrderDirection.ASC
                    ? queryable.OrderBy(x => x.FileName)
                    : queryable.OrderByDescending(x => x.FileName),
                _ => throw new ArgumentOutOfRangeException()
            };


            queryable = queryable.Skip(query.Offset).Take(query.PageSize);

            var data = await queryable.Select(x => new GetLegalDocumentsResponseDto

            {
                DocumentId = x.DocumentId,
                DocumentUrl = x.DocumentUrl,
                DocumentType = x.DocumentType,
                FileName = x.FileName,
                Description = x.Description,
                AuctionId = x.AuctionId
            }).AsNoTracking().ToListAsync();

            var result = new ListResponseBaseDto<GetLegalDocumentsResponseDto>
            {
                Data = data,
                Total = await _legalDocumentRepository.GetCount(expression),
                PageSize = query.PageSize,
                Page = query.Page
            };
            
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
     public async Task<ListResponseBaseDto<GetLegalDocumentsResponseDto>> GetLegalDocuments(LegalDocumentQuery query)
        {
            try
            {
                var queryable = _legalDocumentRepository.GetLegalDocumentQuery();
    
                Expression<Func<LegalDocument, bool>> expression = x => x.FileName.Contains(query.FileName);
    
    
                queryable = queryable.Where(expression);
    
                queryable = query.SortBy switch
                {
                    LegalDocumentSortBy.FileName => query.OrderDirection == OrderDirection.ASC
                        ? queryable.OrderBy(x => x.FileName)
                        : queryable.OrderByDescending(x => x.FileName),
                    _ => throw new ArgumentOutOfRangeException()
                };
    
    
                queryable = queryable.Skip(query.Offset).Take(query.PageSize);
    
                var data = await queryable.Select(x => new GetLegalDocumentsResponseDto
    
                {
                    DocumentId = x.DocumentId,
                    DocumentUrl = x.DocumentUrl,
                    DocumentType = x.DocumentType,
                    FileName = x.FileName,
                    Description = x.Description,
                    AuctionId = x.AuctionId
                }).AsNoTracking().ToListAsync();
    
                var result = new ListResponseBaseDto<GetLegalDocumentsResponseDto>
                {
                    Data = data,
                    Total = await _legalDocumentRepository.GetCount(expression),
                    PageSize = query.PageSize,
                    Page = query.Page
                };
                
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
}