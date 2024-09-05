using Jimy.Business.Abstracition;
using Jimy.Business.DTOs;

namespace Jimy.Business.Queries;

public record GetUserByEmail(string Email) : IQuery<UserDto>;