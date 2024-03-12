using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dtos.Response
{
    public class GetBidResponseDto
    {
        public Guid BidId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Guid MemberId { get; set; }
        public Member Member { get; set; }
        public Guid AuctionId { get; set; }
        public Auction Auction { get; set; }
        public bool IsWinningBid { get; set; }
        public Transaction Transaction { get; set; }
    }
}
