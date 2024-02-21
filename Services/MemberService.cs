using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Services;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepository;

    public MemberService(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public Task<Member?> LoginAsync(string username, string password)
    {
        try
        {
            var result = _memberRepository.GetMemberAsync(x => x.Username == username && x.Password == password);
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<ListResponseBaseDto<MemberListResponseDto>> GetMembersAsync(MemberQuery request)
    {
        try
        {
            var offset = request.Offset;
            var pageSize = request.PageSize;
            var page = request.Page;

            var query = _memberRepository.GetMemberQuery();

            if (!string.IsNullOrEmpty(request.Search?.Username))
                query = query.Where(x => x.Username.Contains(request.Search.Username));

            query = request.SortBy switch
            {
                MemberSortBy.Username => request.OrderDirection == OrderDirection.ASC
                    ? query.OrderBy(x => x.Username)
                    : query.OrderByDescending(x => x.Username),
                _ => throw new ArgumentOutOfRangeException()
            };

            query = query.Skip(offset).Take(pageSize);

            var data = await query
                .Select(x => new MemberListResponseDto
                {
                    UserId = x.UserId,
                    Username = x.Username,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber,
                    DateOfBirth = x.DateOfBirth
                })
                .ToListAsync();

            var total = await _memberRepository.GetCountMemberAsync(request.Search);

            var result = new ListResponseBaseDto<MemberListResponseDto>
            {
                Data = data,
                Total = total,
                PageSize = pageSize,
                Page = page
            };
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }


    public async Task UpdateMemberAsync(Guid id, UpdateMemberRequestDto updateMemberRequestDto)
    {
        var toBeUpdated = await _memberRepository.GetMemberAsync(x => x.UserId == id);

        if (toBeUpdated == null) throw new Exception("Member not found");

        toBeUpdated.Username = updateMemberRequestDto.Username ?? toBeUpdated.Username;
        toBeUpdated.Email = updateMemberRequestDto.Email ?? toBeUpdated.Email;
        toBeUpdated.FirstName = updateMemberRequestDto.FirstName ?? toBeUpdated.FirstName;
        toBeUpdated.LastName = updateMemberRequestDto.LastName ?? toBeUpdated.LastName;
        toBeUpdated.PhoneNumber = updateMemberRequestDto.PhoneNumber ?? toBeUpdated.PhoneNumber;
        toBeUpdated.DateOfBirth = updateMemberRequestDto.DateOfBirth ?? toBeUpdated.DateOfBirth;

        await _memberRepository.UpdateMemberAsync(toBeUpdated);
    }

    public async Task DeleteMemberAsync(Guid id)
    {
        try
        {
            var toBeDeleted = await _memberRepository.GetMemberAsync(x => x.UserId == id);

            if (toBeDeleted == null) throw new Exception("Member not found");

            await _memberRepository.DeleteMemberAsync(toBeDeleted);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}