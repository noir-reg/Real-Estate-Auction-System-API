using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using BusinessObjects.Entities;
using Repositories;

namespace Services;

public class AuctionService : IAuctionService
{
    private readonly IAuctionRepository _auctionRepository;

    public AuctionService(IAuctionRepository auctionRepository)
    {
        _auctionRepository = auctionRepository;
    }

    // public Task<ListResponseDto<Auction>> GetAuctions(ListRequestDto<Auction> dto)
    // {
    //     try
    //     {
    //         var result = _auctionRepository.GetAuctions(dto);
    //         return result;
    //     }
    //     catch (Exception e)
    //     {
    //         throw new Exception(e.Message);
    //     }
    // }
    public async Task<ResultResponse<CreateAuctionResponseDto>> CreateAuction(CreateAuctionRequestDto request)
    {
        try
        {
            var toBeAdded = new Auction
            {
                Title = request.Title,
                Status = "Active",
                Description = request.Description,
                RealEstateId = request.RealEstateId,
                RegistrationPeriodStart = request.RegistrationPeriodStart,
                RegistrationPeriodEnd = request.RegistrationPeriodEnd,
                InitialPrice = request.InitialPrice,
                ListingDate = DateTime.Now,
                AuctionPeriodStart = request.AuctionPeriodStart,
                AuctionPeriodEnd = request.AuctionPeriodEnd,
                IncrementalPrice = request.IncrementalPrice,
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
                    RealEstateId = data.RealEstateId,
                    RegistrationPeriodStart = data.RegistrationPeriodStart,
                    RegistrationPeriodEnd = data.RegistrationPeriodEnd,
                    InitialPrice = data.InitialPrice,
                    ListingDate = data.ListingDate,
                    AuctionPeriodStart = data.AuctionPeriodStart,
                    AuctionPeriodEnd = data.AuctionPeriodEnd,
                    IncrementalPrice = data.IncrementalPrice,
                },
                Status = Status.Ok,
                Messages = new[] { "Auction created successfully" }
            };
        }
        catch (Exception e)
        {
            return new ResultResponse<CreateAuctionResponseDto>()
            {
                IsSuccess = false,
                Messages = new[] { e.Message },
                Status = Status.Error
            };
        }
    }
}