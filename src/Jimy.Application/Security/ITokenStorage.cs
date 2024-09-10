using Jimy.Application.DTO;

namespace Jimy.Application.Security;

public interface ITokenStorage
{
    void Set(JwtDto jwt);
    JwtDto Get();
}