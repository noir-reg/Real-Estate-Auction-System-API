using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using BusinessObjects.Entities;

namespace Repositories;

public interface IMemberRepository
{
    Task<List<MemberListResponse>> GetMembersAsync();
    Task AddMemberAsync(Member member);
    Task UpdateMemberAsync(Member member);
    Task DeleteMemberAsync(Member member);
    Task<Member?> GetMemberAsync(string username, string password);
}