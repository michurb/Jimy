using Jimy.Business.Abstracition;
using Jimy.Business.DTOs;

namespace Jimy.Business.Commands;

public record UpdateActivityLog(int Id, UpdateActivityLogDto Dto) : ICommand;