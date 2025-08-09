using Microsoft.AspNetCore.Identity;

namespace eValAPI.Authentication
{
    public class AuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator; // Assume you have an interface for JWT generation

        public AuthService(UserManager<IdentityUser> userManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var result = await _userManager.CheckPasswordAsync(user, password);
            if (!result)
            {
                throw new Exception("Invalid password.");
            }

            // Generate and return a JWT token
            var token = _jwtTokenGenerator.GenerateToken(user);
            return token;
        }
    }
}
