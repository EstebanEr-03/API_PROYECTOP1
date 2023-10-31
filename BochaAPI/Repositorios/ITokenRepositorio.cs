using Microsoft.AspNetCore.Identity;

namespace BochaAPI.Repositorios
{
    public interface ITokenRepositorio
    {
       string CreateJWTTOken(IdentityUser user, List<string> roles);
    }
}
