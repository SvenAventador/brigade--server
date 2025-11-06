using Brigade.Domain.Entities;

namespace Brigade.Domain.Repositories
{
    /// <summary>
    /// Предоставляет методы для управления связями между пользователями и ролями.
    /// </summary>
    public interface IUserRoleRepository
    {
        /// <summary>
        /// Асинхронно добавляет связь между пользователем и ролью в хранилище.
        /// </summary>
        /// <param name="userRole"> Объект, представляющий связь пользователя с ролью. </param>
        /// <param name="cancellationToken"> Токен отмены операции. </param>
        /// <returns> Задача, представляющая асинхронную операцию. </returns>
        Task AddAsync(UserRole userRole, CancellationToken cancellationToken = default);

        /// <summary>
        /// Асинхронно находит связи между пользователем и ролями по идентификатору пользователя.
        /// </summary>
        /// <param name="userId"> Идентификатор пользователя. </param>
        /// <param name="cancellationToken"> Токен отмены операции. </param>
        /// <returns>Коллекция связей пользователя с ролями или null, если не найдены.</returns>
        Task<IEnumerable<UserRole>?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}