using Brigade.Domain.Entities;
using Brigade.Domain.Enums;
using Brigade.Domain.Repositories;
using Brigade.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Brigade.Infrastructure.Repositories
{
    /// <summary>
    /// Реализация репозитория для доступа к ролям пользователей в базе данных.
    /// </summary>
    public class RoleRepository : IRoleRepository
    {
        private readonly BrigadeDbContext _context;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="RoleRepository"/> 
        /// с указанным контекстом базы данных.
        /// </summary>
        /// <param name="context"> Контекст базы данных Entity Framework. </param>
        /// <exception cref="ArgumentNullException"> 
        /// Выбрасывается, если <paramref name="context"/> равен <see langword="null"/>.
        /// </exception>
        public RoleRepository(BrigadeDbContext context)
            => _context = context ?? throw new ArgumentNullException(nameof(context));

        /// <summary>
        /// Асинхронно получает роль по её типу из базы данных.
        /// </summary>
        /// <param name="type"> Тип роли, заданный перечислением <see cref="RoleType"/>. </param>
        /// <param name="cancellationToken"> Токен отмены операции. </param>
        /// <returns>
        /// Экземпляр <see cref="Role"/>, если роль с указанным типом найдена;
        /// в противном случае — <see langword="null"/>.
        /// </returns>
        public async Task<Role?> GetByTypeAsync(RoleType type, CancellationToken cancellationToken = default)
            => await _context.Roles 
                             .FirstOrDefaultAsync(x => x.Type.ToString() == type.ToString(), cancellationToken);

        /// <summary>
        /// Асинхронно получает роль по её идентификатору из базы данных.
        /// </summary>
        /// <param name="id"> Идентификатор роли. </param>
        /// <param name="cancellationToken"> Токен отмены операции. </param>
        /// <returns>
        /// Экземпляр <see cref="Role"/>, если роль с указанным идентификатором найдена;
        /// в противном случае — <see langword="null"/>.
        /// </returns>
        public async Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _context.Roles
                             .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }
}