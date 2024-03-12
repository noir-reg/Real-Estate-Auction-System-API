using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IBidService
    {
        Task<ResultResponse<CreateBidResponseDto>> CreateBid(CreateBidRequestDto request);
        Task<ListResponseBaseDto<GetBidResponseDto>> GetBids(BaseQueryDto request);         
        Task<ResultResponse<GetBidResponseDto>> GetBidById(Guid id);
    }
}
