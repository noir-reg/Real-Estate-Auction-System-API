using System.Linq.Expressions;
using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using BusinessObjects.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Services;

public class AuctionMediaService : IAuctionMediaService
{
    private readonly IAuctionMediaRepository _auctionMediaRepository;

    private readonly IFirebaseStorageService _firebaseStorageService;

    public AuctionMediaService(IAuctionMediaRepository auctionMediaRepository,
        IFirebaseStorageService firebaseStorageService)
    {
        _auctionMediaRepository = auctionMediaRepository;
        _firebaseStorageService = firebaseStorageService;
    }

    public async Task<ResultResponse<UploadMediaResponseDto>> UploadMedia(Guid auctionId, IFormFile file)
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

            var legalDocument = new AuctionMedia()
            {
                AuctionId = auctionId,
                FileName = objectName,
                MediaType = file.ContentType,
                FileUrl = gcsUri,
                Description = file.ContentDisposition
            };

            var data = await _auctionMediaRepository.Add(legalDocument);


            var result = new ResultResponse<UploadMediaResponseDto>()
            {
                Status = Status.Ok,
                Messages = new[] { "Legal Document uploaded successfully" },
                Data = new UploadMediaResponseDto()
                {
                    MediaId = data.MediaId,
                    FileName = data.FileName,
                    FileUrl = data.FileUrl,
                    MediaType = data.MediaType,
                    Description = data.Description
                },
                IsSuccess = true
            };

            return result;
        }
        catch (Exception e)
        {
            return ErrorResponse.CreateErrorResponse<UploadMediaResponseDto>(e);
        }
    }

    public async Task<ListResponseBaseDto<GetAuctionMediasResponseDto>> GetMedia(Guid auctionId,
        AuctionMediaQuery query)
    {
        try
        {
            IQueryable<AuctionMedia> queryable = _auctionMediaRepository.GetQueryable();
            Expression<Func<AuctionMedia, bool>> predicate;

            if (query.Search?.FileName != null)
            {
                predicate = x => x.FileName.Contains(query.Search.FileName) && x.AuctionId == auctionId;
            }
            else
            {
                predicate = x => x.AuctionId == auctionId;
            }

            queryable = queryable.Where(predicate);
            queryable = queryable.Skip(query.Offset).Take(query.PageSize);
            
            var result = new ListResponseBaseDto<GetAuctionMediasResponseDto>
            {
                Data = await queryable
                    .Select(x => new GetAuctionMediasResponseDto
                    {
                        MediaId = x.MediaId,
                        FileName = x.FileName,
                        FileUrl = x.FileUrl,
                        MediaType = x.MediaType,
                        Description = x.Description
                    }).ToListAsync(),
                Total = await _auctionMediaRepository.Count(predicate)
            };
            return result;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}

public class GetAuctionMediasResponseDto
{
    public Guid MediaId { get; set; }
    public string FileName { get; set; }
    public string FileUrl { get; set; }
    public string MediaType { get; set; }
    public string Description { get; set; }
}