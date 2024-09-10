using Jimy.Application.Abstraction;
using Jimy.Application.DTO;
using Jimy.Application.Queries.Users;
using Jimy.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Jimy.Infrastructure.DAL.Handlers;

internal sealed class GetUserByEmailHandler : IQueryHandler<GetUserByEmail, UserDto>
{
    private readonly JimyDbContext _dbContext;

    public GetUserByEmailHandler(JimyDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<UserDto> HandleAsync(GetUserByEmail query)
    {
        var email = new Email(query.Email.Value);
        var user = await _dbContext.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Email == email);

        return user?.AsDto();
    }
}