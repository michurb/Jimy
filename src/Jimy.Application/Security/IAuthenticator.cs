using Jimy.Application.DTO;

namespace Jimy.Application.Security;

public interface IAuthenticator
{
    JwtDto CreateToken(Guid userId, string role);
}