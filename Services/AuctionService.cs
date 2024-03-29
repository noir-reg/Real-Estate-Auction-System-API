﻿using BusinessObjects.Dtos.Request;
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
                Status = request.Status,
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
                    RegistrationPeriodStart = data.RegistrationPeriodStart.ToString(),
                    RegistrationPeriodEnd = data.RegistrationPeriodEnd.ToString(),
                    InitialPrice = data.InitialPrice,
                    ListingDate = data.ListingDate.ToString(),
                    AuctionPeriodStart = data.AuctionPeriodStart.ToString(),
                    AuctionPeriodEnd = data.AuctionPeriodEnd.ToString(),
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
                Address = x.Address,
                AuctionPeriodStart = x.AuctionPeriodStart,
                AuctionPeriodEnd = x.AuctionPeriodEnd,
                IncrementalPrice = x.IncrementalPrice,
                ThumbnailUrl = x.ThumbnailUrl,
                Owner = x.Owner,
                AuctionMedias = x.AuctionMedias.ToList(),
                LegalDocuments = x.LegalDocuments.ToList()
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
                        Status = x.Status,
                        ListingDate = x.ListingDate
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

    public async Task<ResultResponse<UpdateAuctionResponseDto>> UpdateAuction(Guid auctionId,
        UpdateAuctionRequestDto request)
    {
        try
        {
            var toBeUpdated = await _auctionRepository.GetAuction(x => x.AuctionId == auctionId);
            if (toBeUpdated == null)
            {
                return ErrorResponse.CreateErrorResponse<UpdateAuctionResponseDto>(status: Status.NotFound,
                    message: "Auction not found");
            }

            toBeUpdated.Title = !string.IsNullOrEmpty(request.Title) ? request.Title : toBeUpdated.Title;
            toBeUpdated.Description = !string.IsNullOrEmpty(request.Description)
                ? request.Description
                : toBeUpdated.Description;
            toBeUpdated.RealEstateCode = !string.IsNullOrEmpty(request.RealEstateCode)
                ? request.RealEstateCode
                : toBeUpdated.RealEstateCode;
            toBeUpdated.Address = !string.IsNullOrEmpty(request.Address) ? request.Address : toBeUpdated.Address;
            toBeUpdated.ThumbnailUrl = !string.IsNullOrEmpty(request.ThumbnailUrl)
                ? request.ThumbnailUrl
                : toBeUpdated.ThumbnailUrl;
            toBeUpdated.RegistrationPeriodStart =
                request.RegistrationPeriodStart ?? toBeUpdated.RegistrationPeriodStart;
            toBeUpdated.RegistrationPeriodEnd = request.RegistrationPeriodEnd ?? toBeUpdated.RegistrationPeriodEnd;
            toBeUpdated.InitialPrice = request.InitialPrice ?? toBeUpdated.InitialPrice;
            toBeUpdated.ListingDate = request.ListingDate ?? toBeUpdated.ListingDate;
            toBeUpdated.AuctionPeriodStart = request.AuctionPeriodStart ?? toBeUpdated.AuctionPeriodStart;
            toBeUpdated.AuctionPeriodEnd = request.AuctionPeriodEnd ?? toBeUpdated.AuctionPeriodEnd;
            toBeUpdated.IncrementalPrice = request.IncrementalPrice ?? toBeUpdated.IncrementalPrice;
            toBeUpdated.Status = !string.IsNullOrEmpty(request.Status) ? request.Status : toBeUpdated.Status;
            toBeUpdated.OwnerId = request.OwnerId ?? toBeUpdated.OwnerId;

            await _auctionRepository.UpdateAuction(toBeUpdated);

            return new ResultResponse<UpdateAuctionResponseDto>()
            {
                IsSuccess = true,
                Data = new UpdateAuctionResponseDto
                {
                    AuctionId = toBeUpdated.AuctionId,
                    Title = toBeUpdated.Title,
                    Description = toBeUpdated.Description,
                    RealEstateCode = toBeUpdated.RealEstateCode,
                    Address = toBeUpdated.Address,
                    ThumbnailUrl = toBeUpdated.ThumbnailUrl,
                    RegistrationPeriodStart = toBeUpdated.RegistrationPeriodStart,
                    RegistrationPeriodEnd = toBeUpdated.RegistrationPeriodEnd,
                    InitialPrice = toBeUpdated.InitialPrice,
                    ListingDate = toBeUpdated.ListingDate,
                    AuctionPeriodStart = toBeUpdated.AuctionPeriodStart,
                    AuctionPeriodEnd = toBeUpdated.AuctionPeriodEnd,
                    IncrementalPrice = toBeUpdated.IncrementalPrice,
                    Status = toBeUpdated.Status,
                    OwnerId = toBeUpdated.OwnerId
                },
                Status = Status.Ok,
                Messages = new[] { "Auction updated successfully" }
            };
        }
        catch (Exception e)
        {
            return ErrorResponse.CreateErrorResponse<UpdateAuctionResponseDto>(e);
        }
    }

    public async Task<ResultResponse<DeleteAuctionResponseDto>> DeleteAuction(Guid auctionId)
    {
        try
        {
            var toBeDeleted = await _auctionRepository.GetAuction(x => x.AuctionId == auctionId);

            if (toBeDeleted == null)
                return ErrorResponse.CreateErrorResponse<DeleteAuctionResponseDto>(status: Status.NotFound,
                    message: "Auction not found");
            
            await _auctionRepository.DeleteAuction(toBeDeleted);

            return new ResultResponse<DeleteAuctionResponseDto>()
            {
                IsSuccess = true,
                Data = new DeleteAuctionResponseDto
                {
                    AuctionId = toBeDeleted.AuctionId,
                    Title = toBeDeleted.Title,
                    Description = toBeDeleted.Description,
                    RealEstateCode = toBeDeleted.RealEstateCode,
                    Address = toBeDeleted.Address,
                    ThumbnailUrl = toBeDeleted.ThumbnailUrl,
                    RegistrationPeriodStart = toBeDeleted.RegistrationPeriodStart,
                    RegistrationPeriodEnd = toBeDeleted.RegistrationPeriodEnd,
                    InitialPrice = toBeDeleted.InitialPrice,
                    ListingDate = toBeDeleted.ListingDate,
                    AuctionPeriodStart = toBeDeleted.AuctionPeriodStart,
                    AuctionPeriodEnd = toBeDeleted.AuctionPeriodEnd,
                    IncrementalPrice = toBeDeleted.IncrementalPrice,
                    Status = toBeDeleted.Status,
                    OwnerId = toBeDeleted.OwnerId
                },
                Status = Status.Ok,
                Messages = new[] { "Auction deleted successfully" }
            };
        }
        catch (Exception e)
        {
            return ErrorResponse.CreateErrorResponse<DeleteAuctionResponseDto>(e);
        }
    }
}