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
    }
}