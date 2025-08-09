using Microsoft.AspNetCore.Identity;

namespace eValAPI.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(IdentityUser user);
    }
}
