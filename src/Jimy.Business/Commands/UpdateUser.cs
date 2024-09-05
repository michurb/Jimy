using Jimy.Business.Abstracition;
using Jimy.Business.DTOs;

namespace Jimy.Business.Commands;

public record UpdateUser(Guid Id, UpdateUserDto Dto) : ICommand;