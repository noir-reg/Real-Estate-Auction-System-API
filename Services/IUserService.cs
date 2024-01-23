﻿using BusinessObjects.Entities;

namespace Services;

public interface IUserService
{
    Task AddAsync(User newUser);
    Task<List<User>> GetAll();
    Task<bool> Login(string username, string password);
}