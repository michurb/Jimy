using Jimy.Application.Abstraction;
using Jimy.Application.DTO;
using Jimy.Application.Queries.Users;
using Microsoft.EntityFrameworkCore;

namespace Jimy.Infrastructure.DAL.Handlers;

internal sealed class GetUsersHandler : IQueryHandler<GetUsers, IEnumerable<UserDto>>
{
    private readonly JimyDbContext _dbContext;

    public GetUsersHandler(JimyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<UserDto>> HandleAsync(GetUsers query)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .Select(x => x.AsDto())
            .ToListAsync();
    }
}