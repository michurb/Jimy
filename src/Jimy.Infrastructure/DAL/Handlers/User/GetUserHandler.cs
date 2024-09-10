using Jimy.Application.Abstraction;
using Jimy.Application.DTO;
using Jimy.Application.Queries.Users;
using Jimy.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Jimy.Infrastructure.DAL.Handlers;

internal sealed class GetUserHandler : IQueryHandler<GetUser, UserDto>
{
    private readonly JimyDbContext _dbContext;
    
    public GetUserHandler(JimyDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<UserDto> HandleAsync(GetUser query)
    {
        var userId = new UserId(query.UserId);
        var user = await _dbContext.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == userId);

        return user?.AsDto();
    }
}