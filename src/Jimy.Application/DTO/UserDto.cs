﻿namespace Jimy.Application.DTO;

public class UserDto
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public DateTime DateJoined { get; set; }
}