using Microsoft.AspNetCore.Identity;

namespace CursoNET7API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, IEnumerable<string> roles);
    }
}
