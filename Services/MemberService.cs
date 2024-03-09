using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
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
                    DateOfBirth = x.DateOfBirth.ToShortDateString(),
                    Gender = x.Gender,
                    CitizenId = x.CitizenId,
                    Role = x.Role
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


    public async Task<ResultResponse<UpdateMemberResponseDto>> UpdateMemberAsync(Guid id,
        UpdateMemberRequestDto updateMemberRequestDto)
    {
        try
        {
            var toBeUpdated = await _memberRepository.GetMemberAsync(x => x.UserId == id);

            if (toBeUpdated == null)
            {
                var failedResult = new ResultResponse<UpdateMemberResponseDto>
                {
                    IsSuccess = false,
                    Messages = new[] { "Member not found" },
                    Status = Status.NotFound
                };

                return failedResult;
            }

            toBeUpdated.Username = updateMemberRequestDto.Username ?? toBeUpdated.Username;
            toBeUpdated.Email = updateMemberRequestDto.Email ?? toBeUpdated.Email;
            toBeUpdated.FirstName = updateMemberRequestDto.FirstName ?? toBeUpdated.FirstName;
            toBeUpdated.LastName = updateMemberRequestDto.LastName ?? toBeUpdated.LastName;
            toBeUpdated.PhoneNumber = updateMemberRequestDto.PhoneNumber ?? toBeUpdated.PhoneNumber;
            toBeUpdated.DateOfBirth = updateMemberRequestDto.DateOfBirth ?? toBeUpdated.DateOfBirth;
            toBeUpdated.Gender = updateMemberRequestDto.Gender ?? toBeUpdated.Gender;

            await _memberRepository.UpdateMemberAsync(toBeUpdated);

            var data = new UpdateMemberResponseDto
            {
                MemberId = toBeUpdated.UserId,
                Username = toBeUpdated.Username,
                Email = toBeUpdated.Email,
                FirstName = toBeUpdated.FirstName,
                LastName = toBeUpdated.LastName,
                PhoneNumber = toBeUpdated.PhoneNumber,
                DateOfBirth = toBeUpdated.DateOfBirth,
                Gender = toBeUpdated.Gender,
                CitizenId = toBeUpdated.CitizenId,
                Role = toBeUpdated.Role
            };

            var successResult = new ResultResponse<UpdateMemberResponseDto>
            {
                IsSuccess = true,
                Data = data,
                Messages = new[] { "Member updated successfully" },
                Status = Status.Ok
            };

            return successResult;
        }
        catch (Exception e)
        {
            return new ResultResponse<UpdateMemberResponseDto>()
            {
                IsSuccess = false,
                Messages = new[] { e.Message, e.InnerException?.Message },
                Status = Status.Error
            };
        }
    }

    public async Task<ResultResponse<DeleteMemberResponseDto>> DeleteMemberAsync(Guid id)
    {
        try
        {
            var toBeDeleted = await _memberRepository.GetMemberAsync(x => x.UserId == id);

            if (toBeDeleted == null)
            {
                var failedResult = new ResultResponse<DeleteMemberResponseDto>
                {
                    IsSuccess = false,
                    Messages = new[] { "Member not found" },
                    Status = Status.NotFound
                };
                return failedResult;
            }

            var data = new DeleteMemberResponseDto
            {
                MemberId = toBeDeleted.UserId,
                Username = toBeDeleted.Username,
                Email = toBeDeleted.Email,
                FirstName = toBeDeleted.FirstName,
                LastName = toBeDeleted.LastName,
                PhoneNumber = toBeDeleted.PhoneNumber,
                DateOfBirth = toBeDeleted.DateOfBirth,
                Gender = toBeDeleted.Gender,
                CitizenId = toBeDeleted.CitizenId,
                Role = toBeDeleted.Role
            };
            var successResult = new ResultResponse<DeleteMemberResponseDto>
            {
                IsSuccess = true,
                Data = data,
                Messages = new[] { "Member deleted successfully" },
                Status = Status.Ok
            };

            await _memberRepository.DeleteMemberAsync(toBeDeleted);

            return successResult;
        }
        catch (Exception e)
        {
            return new ResultResponse<DeleteMemberResponseDto>()
            {
                IsSuccess = false,
                Messages = new[] { e.Message, e.InnerException?.Message },
                Status = Status.Error
            };
        }
    }

    public async Task<ResultResponse<MemberDetailResponseDto>> GetMemberAsync(Guid id)
    {
        try
        {
            var data = await _memberRepository.GetMemberAsync(x => x.UserId == id);

            if (data == null)
            {
                var failedResult = new ResultResponse<MemberDetailResponseDto>
                {
                    IsSuccess = false,
                    Messages = new[] { "Member not found" },
                    Status = Status.NotFound
                };
                return failedResult;
            }

            var result = new MemberDetailResponseDto
            {
                MemberId = data.UserId,
                Username = data.Username,
                Email = data.Email,
                FirstName = data.FirstName,
                LastName = data.LastName,
                PhoneNumber = data.PhoneNumber,
                DateOfBirth = data.DateOfBirth,
                Gender = data.Gender,
                CitizenId = data.CitizenId,
                Role = data.Role
            };

            var successResult = new ResultResponse<MemberDetailResponseDto>
            {
                IsSuccess = true,
                Data = result,
                Messages = new[] { "Member found successfully" },
                Status = Status.Ok
            };

            return successResult;
        }
        catch (Exception e)
        {
            return new ResultResponse<MemberDetailResponseDto>()
            {
                IsSuccess = false,
                Messages = new[] { e.Message, e.InnerException?.Message },
                Status = Status.Error
            };
        }
    }
}