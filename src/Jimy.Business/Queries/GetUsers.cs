using Jimy.Business.Abstracition;
using Jimy.Business.DTOs;

namespace Jimy.Business.Queries;

public record GetUsers : IQuery<IEnumerable<UserDto>>;