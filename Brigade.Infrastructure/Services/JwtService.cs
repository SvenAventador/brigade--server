using Brigade.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Brigade.Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly string _signingKey;
        private readonly int _accessTokenExpirationMinutes;
        private readonly int _refreshTokenExpirationDays;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
            _issuer = _configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("Jwt:Issuer не настроен.");
            _audience = _configuration["Jwt:Audience"] ?? throw new InvalidOperationException("Jwt:Audience не настроен.");
            _signingKey = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key не настроен.");
            _accessTokenExpirationMinutes = int.Parse(_configuration["Jwt:AccessTokenExpirationMinutes"] ?? "30");
            _refreshTokenExpirationDays = int.Parse(_configuration["Jwt:RefreshTokenExpirationDays"] ?? "7");
        }

        /// <inheritdoc />
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        /// <inheritdoc />
        public string GenerateToken(Guid userId, 
                                    string email, 
                                    IEnumerable<string> roles, 
                                    IEnumerable<KeyValuePair<string, string>>? claims = null)
        {
            var signingCredentials = GetSigningCredentials();
            var claimsIdentity = GenerateClaimsIdentity(userId, email, roles, claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddMinutes(_accessTokenExpirationMinutes),
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = signingCredentials,
                TokenType = "JWT"
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /// <inheritdoc />
        public Guid? GetUserIdFromToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = GetValidationParameters();
                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
                var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier); 
                if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var userId))
                {
                    return userId;
                }
            }
            catch (SecurityTokenException)
            {
                
            }
            return null; 
        }

        /// <inheritdoc />
        public bool ValidateToken(string token, out ClaimsPrincipal? claimsPrincipal)
        {
            claimsPrincipal = null;
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = GetValidationParameters();
                var validatedToken = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken rawValidatedToken);

                if (rawValidatedToken is JwtSecurityToken jwtToken)
                {
                    var tokenTypeClaim = jwtToken.Payload["typ"] as string;
                    if (tokenTypeClaim != "JWT") 
                        return false;

                    var expectedClaims = new[] { 
                        ClaimTypes.NameIdentifier,
                        ClaimTypes.Email 
                    }; 

                    foreach (var expectedClaim in expectedClaims)
                        if (!jwtToken.Payload.ContainsKey(expectedClaim))
                            return false;
                }
                else
                    return false;

                claimsPrincipal = validatedToken;
                return true;
            }
            catch (SecurityTokenException)
            {
                return false;
            }
        }

        /// <summary>
        /// Создаёт объект <see cref="SigningCredentials"/> для подписи токена.
        /// </summary>
        /// <returns> Объект <see cref="SigningCredentials"/>. </returns>
        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_signingKey);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        /// <summary>
        /// Создаёт объект <see cref="ClaimsIdentity"/> с основными и дополнительными утверждениями.
        /// </summary>
        /// <param name="userId"> Идентификатор пользователя. </param>
        /// <param name="email"> Email пользователя. </param>
        /// <param name="roles"> Коллекция ролей пользователя. </param>
        /// <param name="claims"> Дополнительные утверждения. </param>
        /// <returns> Объект <see cref="ClaimsIdentity"/>. </returns>
        private ClaimsIdentity GenerateClaimsIdentity(Guid userId, 
                                                      string email, 
                                                      IEnumerable<string> roles, 
                                                      IEnumerable<KeyValuePair<string, string>>? claims)
        {
            var claimsIdentity = new ClaimsIdentity();

            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId.ToString())); 
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, email)); 

            foreach (var role in roles)
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role)); 

            if (claims != null)
                foreach (var claim in claims)
                    claimsIdentity.AddClaim(new Claim(claim.Key, claim.Value));

            return claimsIdentity;
        }

        /// <summary>
        /// Создаёт объект <see cref="TokenValidationParameters"/> для валидации токена.
        /// </summary>
        /// <returns> Объект <see cref="TokenValidationParameters"/>. </returns>
        private TokenValidationParameters GetValidationParameters()
        {
            var key = Encoding.UTF8.GetBytes(_signingKey);
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _issuer, 

                ValidateAudience = true, 
                ValidAudience = _audience, 

                ValidateLifetime = true, 

                ValidateIssuerSigningKey = true, 
                IssuerSigningKey = new SymmetricSecurityKey(key), 

                ClockSkew = TimeSpan.Zero
            };
        }
    }
}