using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using BusinessObjects.Entities;

namespace Services;

public interface IMemberService
{
    Task<Member?> LoginAsync(string username, string password);
    Task<ListResponseBaseDto<MemberListResponseDto>> GetMembersAsync(MemberQuery request);
    Task UpdateMemberAsync(UpdateMemberRequestDto updateMemberRequestDto);
    Task DeleteMemberAsync(Guid id);
}