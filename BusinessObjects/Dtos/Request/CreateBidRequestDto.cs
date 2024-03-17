using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dtos.Request
{
    public class CreateBidRequestDto
    {
        [Required (ErrorMessage = "Please enter amount")]
        public decimal Amount { get; set; }
        [Required (ErrorMessage = "Please enter memberId")]
        public Guid MemberId { get; set; }
        [Required (ErrorMessage = "Please enter auctionId")]
        public Guid AuctionId { get; set; }
    }
}
