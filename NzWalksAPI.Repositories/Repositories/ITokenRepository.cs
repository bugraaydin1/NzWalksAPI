using Microsoft.AspNetCore.Identity;

namespace NzWalksAPI.Repositories.Repositories
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}