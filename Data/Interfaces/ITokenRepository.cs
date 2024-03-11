using Microsoft.AspNetCore.Identity;

namespace API.Data.Interfaces
{
    public interface ITokenRepository
    {
        string CreateToken(IdentityUser user, List<string> roles);
    }
}