﻿using BusinessObjects.Enums;

namespace BusinessObjects.Entities;

public class User
{
    public Guid UserId { get; set; }

    public string Password { get; set; }

    // public int RoleId { get; set; }
    // public Role Role { get; set; }
    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string CitizenId { get; set; }
    public string Email { get; set; }
}