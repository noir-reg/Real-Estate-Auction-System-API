using System.Linq.Expressions;
using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using BusinessObjects.Entities;

namespace Repositories;

public interface IMemberRepository
{
    Task<List<MemberListResponseDto>> GetMembersAsync();
    Task AddMemberAsync(Member member);
    Task UpdateMemberAsync(Member member);
    Task DeleteMemberAsync(Member member);
    Task<Member?> GetMemberAsync(Expression<Func<Member, bool>> predicate);
    IQueryable<Member> GetMemberQuery();
    Task<int> GetCountMemberAsync(SearchMemberQuery request);
}