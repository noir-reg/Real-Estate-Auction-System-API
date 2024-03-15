using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Services;

public class AuctionService : IAuctionService
{
    private readonly IAuctionRepository _auctionRepository;
    private readonly ILegalDocumentRepository _documentRepository;
    private readonly IRealEstateRepository _realEstateRepository;

    public AuctionService(IAuctionRepository auctionRepository, ILegalDocumentRepository documentRepository, IRealEstateRepository realEstateRepository)
    {
        _auctionRepository = auctionRepository;
        _documentRepository = documentRepository;
        _realEstateRepository = realEstateRepository;
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

    public async Task<ResultResponse<AuctionPostDetailResponseDto>> GetAuctionById(Guid auctionId)
    {
        try
        {
            Auction? auction = await _auctionRepository.GetAuction(x=>x.AuctionId == auctionId); 
            
            if (auction == null)
            {
                return new ResultResponse<AuctionPostDetailResponseDto>()
                {
                    IsSuccess = false,
                    Status = Status.NotFound,
                    Messages = new[] { "Auction not found" }
                };
            }

            var realEstate = await _realEstateRepository.GetRealEstate(x => x.AuctionId == auctionId);
            
            if (realEstate == null)
            {
                return new ResultResponse<AuctionPostDetailResponseDto>()
                {
                    IsSuccess = false,
                    Status = Status.NotFound,
                    Messages = new[] { "RealEstate not found" }
                };
            }
            
            var legalDocuments = await _documentRepository.GetLegalDocuments(x => x.RealEstateId == realEstate.RealEstateId);
            
            var data = new AuctionPostDetailResponseDto
            {
                AuctionId = auction.AuctionId,
                Title = auction.Title,
                Description = auction.Description,
                RealEstateId = realEstate.RealEstateId,
                InitialPrice = auction.InitialPrice,
                AuctionPeriodStart = auction.AuctionPeriodStart,
                AuctionPeriodEnd = auction.AuctionPeriodEnd,
                IncrementalPrice = auction.IncrementalPrice,
                LegalDocuments = legalDocuments
            };

            return new ResultResponse<AuctionPostDetailResponseDto>()
            {
                IsSuccess = true,
                Data = data,
                Status = Status.Ok,
                Messages = new[] { "Get auction successfully" }
            };
        }
        catch (Exception e)
        {
            return new ResultResponse<AuctionPostDetailResponseDto>()
            {
                IsSuccess = false,
                Messages = new[] { e.Message, e.InnerException?.Message },
                Status = Status.Error
            };
        }
    }
}