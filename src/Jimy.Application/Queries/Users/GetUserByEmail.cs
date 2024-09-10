using Jimy.Application.Abstraction;
using Jimy.Application.DTO;
using Jimy.Core.ValueObjects;

namespace Jimy.Application.Queries.Users;

public class GetUserByEmail : IQuery<UserDto>
{
    public Email Email { get; set; }
}