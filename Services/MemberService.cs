using BusinessObjects.Dtos.Response;
using BusinessObjects.Entities;
using Repositories;

namespace Services;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepository;

    public MemberService(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public Task<Member?> GetMember(string username, string password)
    {
        try
        {
            var result = _memberRepository.GetMemberAsync(username, password);
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<List<MemberListResponse>> GetMembersAsync()
    {
        try
        {
            var result = _memberRepository.GetMembersAsync();
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task AddMemberAsync(Member member)
    {
        try
        {
            var result = _memberRepository.AddMemberAsync(member);
            return result;
        }

        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task UpdateMemberAsync(Member member)
    {
        try
        {
            var result = _memberRepository.UpdateMemberAsync(member);
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task DeleteMemberAsync(Member member)
    {
        try
        {
            var result = _memberRepository.DeleteMemberAsync(member);
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}