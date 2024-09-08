﻿namespace Jimy.Data.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public DateTime DateJoined { get; set; } = DateTime.UtcNow;
}