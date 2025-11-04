using Brigade.Domain.Services;
using System.Security.Claims;

namespace Brigade.Infrastructure.Services
{
    internal class JwtService : IJwtService
    {
        public string GenerateRefreshToken()
        {
            throw new NotImplementedException();
        }

        public string GenerateToken(Guid userId, string email, IEnumerable<string> roles, IEnumerable<KeyValuePair<string, string>>? claims = null)
        {
            throw new NotImplementedException();
        }

        public Guid? GetUserIdFromToken(string token)
        {
            throw new NotImplementedException();
        }

        public bool ValidateToken(string token, out ClaimsPrincipal? claimsPrincipal)
        {
            throw new NotImplementedException();
        }
    }
}