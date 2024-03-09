using System.Linq.Expressions;
using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class MemberRepository : IMemberRepository
{
    private readonly RealEstateDbContext _context;

    public MemberRepository()
    {
        _context = new RealEstateDbContext();
    }

    public Task<List<MemberListResponseDto>> GetMembersAsync()
    {
        try
        {
            var result = _context.Members.Select(e => new MemberListResponseDto
            {
                UserId = e.UserId,
                Username = e.Username,
                Email = e.Email,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Gender = e.Gender,
                DateOfBirth = e.DateOfBirth.ToShortDateString(),
                CitizenId = e.CitizenId
            }).ToListAsync();
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<Member> AddMemberAsync(Member member)
    {
        try
        {
            _context.Members.Add(member)
                ;
            await _context.SaveChangesAsync();
            return member;
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
            _context.Members.Update(member);
            return _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<Member?> GetMemberAsync(Expression<Func<Member, bool>> predicate)
    {
        var result = _context.Members.SingleOrDefaultAsync(predicate)
            ;
        return result;
    }

    public IQueryable<Member> GetMemberQuery()
    {
        return _context.Members.AsQueryable();
    }

    public Task DeleteMemberAsync(Member member)
    {
        try
        {
            _context.Members.Remove(member);
            return _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<int> GetCountMemberAsync(SearchMemberQuery request)
    {
        try
        {
            var query = _context.Members.AsQueryable();

            if (request == null) return query.CountAsync();

            if (!string.IsNullOrEmpty(request.Username))
                query = query.Where(x => x.Username.Contains(request.Username));


            return query.CountAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}