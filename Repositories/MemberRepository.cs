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

    public Task<List<MemberListResponse>> GetMembersAsync()
    {
        try
        {
            var result = _context.Members.Select(e => new MemberListResponse
            {
                UserId = e.UserId,
                Username = e.Username,
                Email = e.Email,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Gender = e.Gender,
                DateOfBirth = e.DateOfBirth,
                CitizenId = e.CitizenId
            }).ToListAsync();
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
            _context.Members.Add(member)
                ;
            return _context.SaveChangesAsync();
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

    public Task<Member?> GetMemberAsync(string username, string password)
    {
        try
        {
            var result = _context.Members.SingleOrDefaultAsync(x => x.Username == username && x.Password == password);
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
            _context.Members.Remove(member);
            return _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}