﻿using BusinessObjects.Dtos.Response;

namespace Repositories;

public interface IUserRepository
{
    Task<UserInfo?> Login(string username, string password);
}