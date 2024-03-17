using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Services;

public class AuctionService : IAuctionService
{
    private readonly IAuctionRepository _auctionRepository;

    public AuctionService(IAuctionRepository auctionRepository)
    {
        _auctionRepository = auctionRepository;
    }


    public async Task<ResultResponse<CreateAuctionResponseDto>> CreateAuction(CreateAuctionRequestDto request)
    {
        try
        {
            var toBeAdded = new Auction
            {
                Title = request.Title,
                Status = AuctionStatus.ToBeSold,
                Description = request.Description,
                RealEstateCode = request.RealEstateCode,
                RegistrationPeriodStart = request.RegistrationPeriodStart,
                RegistrationPeriodEnd = request.RegistrationPeriodEnd,
                InitialPrice = request.InitialPrice,
                ListingDate = DateTime.Now,
                AuctionPeriodStart = request.AuctionPeriodStart,
                AuctionPeriodEnd = request.AuctionPeriodEnd,
                IncrementalPrice = request.IncrementalPrice,
                AdminId = request.AdminId,
                OwnerId = request.OwnerId,
            };

            var data = await _auctionRepository.AddAuction(toBeAdded);

            return new ResultResponse<CreateAuctionResponseDto>()
            {
                IsSuccess = true,
                Data = new CreateAuctionResponseDto
                {
                    AuctionId = data.AuctionId,
                    Title = data.Title,
                    Status = data.Status,
                    Description = data.Description,
                    RealEstateCode = data.RealEstateCode,
                    RegistrationPeriodStart = data.RegistrationPeriodStart,
                    RegistrationPeriodEnd = data.RegistrationPeriodEnd,
                    InitialPrice = data.InitialPrice,
                    ListingDate = data.ListingDate,
                    AuctionPeriodStart = data.AuctionPeriodStart,
                    AuctionPeriodEnd = data.AuctionPeriodEnd,
                    IncrementalPrice = data.IncrementalPrice,
                    ThumbnailUrl = data.ThumbnailUrl
                },
                Status = Status.Ok,
                Messages = new[] { "Auction created successfully" }
            };
        }
        catch (Exception e)
        {
            return ErrorResponse.CreateErrorResponse<CreateAuctionResponseDto>(e);
        }
    }


    public async Task<ResultResponse<AuctionPostDetailResponseDto>> GetAuctionById(Guid auctionId)
    {
        try
        {
            var query = _auctionRepository.GetAuctionQuery();
            query = query.Where(x => x.AuctionId == auctionId);

            query = query.Include(x => x.LegalDocuments)
                .Include(x => x.AuctionMedias)
                .Include(x => x.Owner);

            var data = await query.Select(x => new AuctionPostDetailResponseDto
            {
                AuctionId = x.AuctionId,
                Title = x.Title,
                Status = x.Status,
                Description = x.Description,
                RealEstateCode = x.RealEstateCode,
                RegistrationPeriodStart = x.RegistrationPeriodStart,
                RegistrationPeriodEnd = x.RegistrationPeriodEnd,
                InitialPrice = x.InitialPrice,
                ListingDate = x.ListingDate,
                AuctionPeriodStart = x.AuctionPeriodStart,
                AuctionPeriodEnd = x.AuctionPeriodEnd,
                IncrementalPrice = x.IncrementalPrice,
                ThumbnailUrl = x.ThumbnailUrl,
                Owner = x.Owner,
                AuctionMedias = x.AuctionMedias,
                LegalDocuments = x.LegalDocuments
            }).SingleOrDefaultAsync();

            if (data == null)
            {
                return ErrorResponse.CreateErrorResponse<AuctionPostDetailResponseDto>(message: "Auction not found");
            }

            return new ResultResponse<AuctionPostDetailResponseDto>()
            {
                IsSuccess = true,
                Data = data,
                Status = Status.Ok,
                Messages = new[] { "Auction retrieved successfully" }
            };
        }
        catch (Exception e)
        {
            return ErrorResponse.CreateErrorResponse<AuctionPostDetailResponseDto>(e);
        }
    }

    public async Task<ListResponseBaseDto<AuctionPostListResponseDto>> GetAuctions(AuctionQuery request)
    {
        try
        {
            var query = _auctionRepository.GetAuctionQuery();

            {
                if (!string.IsNullOrEmpty(request.Search?.Title))
                    query = query.Where(x => x.Title.Contains(request.Search.Title));

                query = query.OrderBy(x => x.Title);
                query = query.Skip(request.Offset).Take(request.PageSize);

                var data = await query.Select(
                    x => new AuctionPostListResponseDto()
                    {
                        AuctionId = x.AuctionId,
                        Title = x.Title,
                        InitialPrice = x.InitialPrice,
                        Thumbnail = x.ThumbnailUrl!,
                        AuctionStart = x.AuctionPeriodStart,
                        Status = x.Status
                    }
                ).ToListAsync();

                var totalCount = await _auctionRepository.GetCountAsync(
                    wherePredicate: !string.IsNullOrEmpty(request.Search?.Title)
                        ? x => x.Title.Contains(request.Search.Title)
                        : null);

                return new ListResponseBaseDto<AuctionPostListResponseDto>()
                {
                    Data = data,
                    Page = request.Page,
                    PageSize = request.PageSize,
                    Total = totalCount
                };
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}