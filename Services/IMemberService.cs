using BusinessObjects.Dtos.Response;
using BusinessObjects.Entities;

namespace Services;

public interface IMemberService
{
    Task<Member?> GetMember(string username, string password);
    Task<List<MemberListResponse>> GetMembersAsync();
    Task AddMemberAsync(Member member);
    Task UpdateMemberAsync(Member member);
    Task DeleteMemberAsync(Member member);
}