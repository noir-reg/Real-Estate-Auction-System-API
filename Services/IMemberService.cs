using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;

namespace Services;

public interface IMemberService
{
    Task<ListResponseBaseDto<MemberListResponseDto>> GetMembersAsync(MemberQuery request);

    Task<ResultResponse<UpdateMemberResponseDto>> UpdateMemberAsync(Guid id,
        UpdateMemberRequestDto updateMemberRequestDto);

    Task<ResultResponse<DeleteMemberResponseDto>> DeleteMemberAsync(Guid id);
    Task<ResultResponse<MemberDetailResponseDto>> GetMemberAsync(Guid id);
}