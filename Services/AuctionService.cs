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

    public AuctionService(IAuctionRepository auctionRepository, ILegalDocumentRepository documentRepository)
    {
        _auctionRepository = auctionRepository;
        _documentRepository = documentRepository;
    }

   
    public async Task<ResultResponse<CreateAuctionResponseDto>> CreateAuction(CreateAuctionRequestDto request)
    {
        try
        {
            var toBeAdded = new Auction
            {
                Title = request.Title,
                Status = "Active",
                Description = request.Description,
                RealEstateCode = request.RealEstateCode,
                RegistrationPeriodStart = request.RegistrationPeriodStart,
                RegistrationPeriodEnd = request.RegistrationPeriodEnd,
                InitialPrice = request.InitialPrice,
                ListingDate = DateTime.Now,
                AuctionPeriodStart = request.AuctionPeriodStart,
                AuctionPeriodEnd = request.AuctionPeriodEnd,
                IncrementalPrice = request.IncrementalPrice,
                AdminId = request.AdminId
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
            
            var legalDocuments = await _documentRepository.GetLegalDocuments(x => x.AuctionId == auctionId);
            
            var data = new AuctionPostDetailResponseDto
            {
                AuctionId = auction.AuctionId,
                Title = auction.Title,
                Description = auction.Description,
                InitialPrice = auction.InitialPrice,
                AuctionPeriodStart = auction.AuctionPeriodStart,
                AuctionPeriodEnd = auction.AuctionPeriodEnd,
                IncrementalPrice = auction.IncrementalPrice,
                RealEstateCode = auction.RealEstateCode,
                RealEstateOwnerName = auction.Owner.FullName,
                Address = auction.Address,
                RegistrationPeriodStart = auction.RegistrationPeriodStart,
                RegistrationPeriodEnd = auction.RegistrationPeriodEnd,
                LegalDocuments = legalDocuments,
                AuctionMedias = auction.AuctionMedias
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