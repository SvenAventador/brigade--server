using Brigade.Domain.Entities;
using Brigade.Domain.Repositories;
using Brigade.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Brigade.Infrastructure.Repositories
{
    public class UserRefreshTokenRepository : IUserRefreshTokenRepository
    {
        private readonly BrigadeDbContext _context;

        public UserRefreshTokenRepository(BrigadeDbContext context)
            => _context = context;

        /// <inheritdoc/>
        public async Task AddAsync(UserRefreshToken refreshToken, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(refreshToken);

            await _context.UserRefreshTokens.AddAsync(refreshToken, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<UserRefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException(nameof(token));

            return await _context.UserRefreshTokens
                                 .Include(x => x.User)
                                 .FirstOrDefaultAsync(x => x.RefreshToken == token, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task RevokeByTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException(nameof(token));

            var revokeToken = await GetByTokenAsync(token, cancellationToken);
            revokeToken?.RevokeToken();
             _context.UserRefreshTokens.Update(revokeToken);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(UserRefreshToken refreshToken, CancellationToken cancellationToken = default)
        {
            _context.UserRefreshTokens.Update(refreshToken);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public async Task<UserRefreshToken?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            if (userId == Guid.Empty)
                return null;

            var now = DateTime.UtcNow;
            return await _context.UserRefreshTokens
                                 .Include(x => x.User)
                                 .FirstOrDefaultAsync(x => x.UserId == userId && 
                                                           !x.IsRevoked && 
                                                           x.ExpiresAt > now, cancellationToken);
        }
    }
}