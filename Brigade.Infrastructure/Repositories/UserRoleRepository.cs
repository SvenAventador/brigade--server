using Brigade.Domain.Entities;
using Brigade.Domain.Repositories;
using Brigade.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Brigade.Infrastructure.Repositories
{
    /// <summary>
    /// Реализация репозитория для управления связями между пользователями и ролями (многие-ко-многим).
    /// </summary>
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly BrigadeDbContext _context;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="UserRoleRepository"/> 
        /// с указанным контекстом базы данных.
        /// </summary>
        /// <param name="context"> Контекст базы данных Entity Framework. </param>
        /// <exception cref="ArgumentNullException"> 
        /// Выбрасывается, если <paramref name="context"/> равен <see langword="null"/>.
        /// </exception>
        public UserRoleRepository(BrigadeDbContext context)
            => _context = context ?? throw new ArgumentNullException(nameof(context));

        /// <summary>
        /// Асинхронно добавляет связь между пользователем и ролью в контекст базы данных.
        /// Изменения будут сохранены при вызове <see cref="BrigadeDbContext.SaveChangesAsync"/>.
        /// </summary>
        /// <param name="userRole"> Объект, представляющий связь пользователя с ролью. </param>
        /// <param name="cancellationToken"> Токен отмены операции. </param>
        /// <returns> Задача, представляющая асинхронную операцию. </returns>
        /// <exception cref="ArgumentNullException">
        /// Выбрасывается, если <paramref name="userRole"/> равен <see langword="null"/>.
        /// </exception>
        public async Task AddAsync(UserRole userRole, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(userRole);

            await _context.UserRoles.AddAsync(userRole, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<UserRole>?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            if (userId == Guid.Empty)
                return null;

            return await _context.UserRoles
                                 .Include(ur => ur.Role) 
                                 .Where(ur => ur.UserId == userId)
                                 .ToListAsync(cancellationToken);
        }
    }
}