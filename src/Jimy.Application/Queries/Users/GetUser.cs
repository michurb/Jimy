using Jimy.Application.Abstraction;
using Jimy.Application.DTO;

namespace Jimy.Application.Queries.Users;

public class GetUser : IQuery<UserDto>
{
    public Guid UserId { get; set; }
}