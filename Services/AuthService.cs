﻿using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using BusinessObjects.Entities;
using Microsoft.Extensions.Configuration;
using Repositories;

namespace Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _config;
    private readonly IMemberRepository _memberRepository;
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository, IMemberRepository memberRepository, IConfiguration config)
    {
        _userRepository = userRepository;
        _memberRepository = memberRepository;
        _config = config;
    }

    public async Task<UserInfo?> Login(string email, string password)
    {
        try
        {
            var data = await _userRepository.GetUserAsync(x => email == x.Email && password == x.Password);

            if (data == null) throw new Exception("User not found");

            var userInfo = new UserInfo
            {
                UserId = data.UserId,
                Email = data.Email,
                Username = data.Username,
                Role = data.Role,
                DateOfBirth
                    = data.DateOfBirth,
                Gender = data.Gender,
                PhoneNumber = data.PhoneNumber
            };
            return userInfo;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<ResultResponse<RegisterMemberResponseDto>> Register(RegisterMemberRequestDto dto)
    {
        try
        {
            var member = new Member
            {
                Email = dto.Email,
                Username = dto.Username,
                Password = dto.Password,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Gender = dto.Gender,
                DateOfBirth = dto.DateOfBirth,
                CitizenId = dto.CitizenId,
                PhoneNumber = dto.PhoneNumber
            };

            var email = dto.Email;

            var result = await _userRepository.GetUserAsync(x => x.Email == email || x.Username == dto.Username
                || x.CitizenId == dto.CitizenId || x.PhoneNumber == dto.PhoneNumber);
            if (result != null)
            {
                return ErrorResponse.CreateErrorResponse<RegisterMemberResponseDto>(
                    message: "Email or username or citizen id or phone number already exists",
                    status: Status.Duplicate);
            }


            await _memberRepository.AddMemberAsync(member);

            var newMember = await _memberRepository.GetMemberAsync(x => x.Email == email);
            var data = new RegisterMemberResponseDto
            {
                MemberId = newMember.UserId,
                Email = newMember.Email,
                Username = newMember.Username,
                FirstName = newMember.FirstName,
                LastName = newMember.LastName,
                Gender = newMember.Gender,
                DateOfBirth = newMember.DateOfBirth,
                CitizenId = newMember.CitizenId,
                PhoneNumber = newMember.PhoneNumber
            };

            var successResult = new ResultResponse<RegisterMemberResponseDto>
            {
                IsSuccess = true,
                Data = data,
                Messages = new[] { "Register success" }
            };
            return successResult;
        }
        catch (Exception e)
        {
            return ErrorResponse.CreateErrorResponse<RegisterMemberResponseDto>(e);
        }
    }
}