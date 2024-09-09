using Jimy.Application.DTO;
using MediatR;

namespace Jimy.Application.Users.Queries.GetUsers;

public record GetUsersQuery : IRequest<List<UserDto>>;