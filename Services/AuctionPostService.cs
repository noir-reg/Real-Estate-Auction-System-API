using System.Linq.Expressions;
using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Services;

public class AuctionPostService : IAuctionPostService
{
    private readonly IAuctionRepository _auctionRepository;

    public AuctionPostService(IAuctionRepository auctionRepository)
    {
        _auctionRepository = auctionRepository;
    }

    public async Task<ListResponseBaseDto<AuctionPostListResponseDto>> GetAuctionPosts(AuctionPostsQuery request)
    {
        var query = _auctionRepository.GetAuctionQuery();

        query = query.Include(x => x.RealEstate)
            .ThenInclude(x => x.Owner);

        query = query.Skip(request.Offset).Take(request.PageSize);


        if (!string.IsNullOrEmpty(request.Title))
        {
            query = query.Where(x => x.Title.Contains(request.Title));
        }

        var data = await query
            .Select(x =>
                new AuctionPostListResponseDto
                {
                    Title = x.Title,
                    AuctionId = x.AuctionId,
                    AuctionStart = x.AuctionPeriodStart,
                    InitialPrice = x.InitialPrice,
                    Thumbnail = x.RealEstate.ImageUrl
                })
            .ToListAsync();

        return new ListResponseBaseDto<AuctionPostListResponseDto>
        {
            Data = data,
            Total = await _auctionRepository.GetCountAsync(x=>x.Title.Contains(request.Title)),
            PageSize = request.PageSize,
            Page = request.Page
        };
    }
}

public class RealEstateOwnerResponseDto
{
    public string FullName { get; set; }
}

public class RealEstateResponseDto
{
    public Guid RealEstateId { get; set; }
    public string RealEstateName { get; set; }
    public string Description { get; set; }
    public object Owner { get; set; }
}