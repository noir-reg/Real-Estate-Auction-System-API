﻿using BusinessObjects.Enums;

namespace BusinessObjects.Entities;

public abstract class User
{
    public Guid UserId { get; set; }

    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }

    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string CitizenId { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Role { get; set; }
}