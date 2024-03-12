using Azure.Core;
using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BidService : IBidService
    {
        private readonly IBidRepository _bidRepository;

        public BidService(IBidRepository bidRepository)
        {
            _bidRepository = bidRepository;
        }

        public async Task<ResultResponse<CreateBidResponseDto>> CreateBid(CreateBidRequestDto request)
        {
            try
            {
                var toBeAdded = new Bid()
                {
                    Date = request.Date,
                    Amount = request.Amount,
                    AuctionId = request.AuctionId,
                    MemberId = request.MemberId,
                    IsWinningBid = request.IsWinningBid
                };

                Bid result = await _bidRepository.AddBidAsync(toBeAdded);

                var data = new CreateBidResponseDto
                {
                    BidId = result.BidId,
                    Date = result.Date,
                    Amount = result.Amount,
                    AuctionId = result.AuctionId,
                    MemberId = result.MemberId,
                    IsWinningBid = result.IsWinningBid
                };

                return new ResultResponse<CreateBidResponseDto>
                {
                    IsSuccess = true,
                    Data = data,
                    Messages = new[] { "Created successfully" },
                    Status = Status.Ok
                };
            }
            catch (Exception e)
            {
                return new ResultResponse<CreateBidResponseDto>()
                {
                    IsSuccess = false,
                    Messages = new[] { e.Message },
                    Status = Status.Error
                };
            }
        }

        public async Task<ResultResponse<GetBidResponseDto>> GetBidById(Guid id)
        {
            try
            {
                var query = _bidRepository.GetBidQuery();
                var data = await query.Include(x => x.Transaction).Include(x => x.Auction).Include(x => x.Member).Where(x=>x.BidId.Equals(id)).Select(x => new GetBidResponseDto
                {
                    BidId = x.BidId,
                    Date = x.Date,
                    Amount = x.Amount,
                    AuctionId = x.AuctionId,
                    MemberId = x.MemberId,
                    IsWinningBid = x.IsWinningBid,
                    Auction = x.Auction,
                    Member = x.Member,
                    Transaction = x.Transaction

                }).AsNoTracking().FirstOrDefaultAsync();


                if (data == null)
                {
                    return new ResultResponse<GetBidResponseDto>()
                    {
                        IsSuccess = false,
                        Messages = new[] { "Bid not found" },
                        Status = Status.NotFound
                    };
                }

                return new ResultResponse<GetBidResponseDto>()
                {
                    IsSuccess = true,
                    Data = data,
                    Status = Status.Ok,
                    Messages = new[] { "Get successfully" }
                };

            }
            catch (Exception e)
            {
                return new ResultResponse<GetBidResponseDto>()
                {
                    IsSuccess = false,
                    Messages = new[] { e.Message, e.InnerException?.Message },
                    Status = Status.Error
                };
            }
        }

        public async Task<ListResponseBaseDto<GetBidResponseDto>> GetBids(BaseQueryDto request)
        {
            try
            {
                var query = _bidRepository.GetBidQuery();
                query = query.Skip(request.Offset).Take(request.PageSize);
                var data =
                     await query.Include(x=>x.Transaction).Include(x=>x.Auction).Include(x=>x.Member).Select(x => new GetBidResponseDto
                     {
                         BidId = x.BidId,
                         Date = x.Date,
                         Amount = x.Amount,
                         AuctionId = x.AuctionId,
                         MemberId = x.MemberId,
                         IsWinningBid = x.IsWinningBid,
                         Auction=x.Auction,
                         Member=x.Member,
                         Transaction=x.Transaction
                         
                     }).AsNoTracking().ToListAsync();


                return new ListResponseBaseDto<GetBidResponseDto>
                {
                    Data = data,
                    Total = data.Count(),
                    PageSize = request.PageSize,
                    Page = request.Page
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
