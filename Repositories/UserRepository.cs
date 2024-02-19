﻿using System.Linq.Expressions;
using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class UserRepository : IUserRepository
{
    private readonly RealEstateDbContext _context;

    public UserRepository()
    {
        _context = new RealEstateDbContext();
    }

    public async Task<UserInfo?> Login(string email, string password)
    {
        try
        {
            var admins = await _context.Admins
                .Where(a => a.Email == email && a.Password == password)
                .Select(a => new UserInfo
                {
                    UserId = a.UserId,
                    Email = a.Email,
                    Username = a.Username,
                    Role = nameof(Admin)
                })
                .ToListAsync();

            var members = await _context.Members
                .Where(m => m.Email == email && m.Password == password)
                .Select(m => new UserInfo
                {
                    UserId = m.UserId,
                    Email = m.Email,
                    Username = m.Username,
                    Role = nameof(Member)
                })
                .ToListAsync();

            var staffs = await _context.Staffs
                .Where(s => s.Email == email && s.Password == password)
                .Select(s => new UserInfo
                {
                    UserId = s.UserId,
                    Email = s.Email,
                    Username = s.Username,
                    Role = nameof(Staff)
                })
                .ToListAsync();

            var users = admins.Concat(members).Concat(staffs);
            return users.SingleOrDefault();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<User?> GetUser(Expression<Func<User, bool>> predicate)
    {
        try
        {
            var result = _context.Users.SingleOrDefaultAsync(predicate);
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task Update(User user)
    {
        try
        {
            _context.Users.Update(user);
            return _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<List<UserListResponse>> GetUsersAsync(UserQuery request)
    {
        var query = _context.Users.AsQueryable();

        if (!string.IsNullOrEmpty(request.Search?.Username))
            query = query.Where(x => x.Username.Contains(request.Search.Username));

        query = request.SortBy switch
        {
            UserSortBy.Username => request.OrderDirection == OrderDirection.ASC
                ? query.OrderBy(x => x.Username)
                : query.OrderByDescending(x => x.Username)
        };

        query = query.Skip(request.Offset).Take(request.PageSize);

        var data = query.Select(x => new UserListResponse
        {
            Username = x.Username,
            Email = x.Email,
            Role = x.Role,
            UserId = x.UserId,
            Gender = x.Gender,
            DateOfBirth = x.DateOfBirth,
            CitizenId = x.CitizenId,
            FirstName = x.FirstName,
            LastName = x.LastName
        }).ToListAsync();

        return data;
    }
}