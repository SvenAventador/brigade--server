using Brigade.Domain.Entities;
using Brigade.Domain.Enums;

namespace Brigade.Domain.Repositories
{
    /// <summary>
    /// Предоставляет методы для получения информации о типах ролей из хранилища.
    /// </summary>
    public interface IRoleRepository
    {
        /// <summary>
        /// Асинхронно получает роль по указанному типу.
        /// </summary>
        /// <param name="type"> Тип роли, для которой требуется получить данные. </param>
        /// <param name="cancellationToken"> Токен отмены операции. </param>
        /// <returns>
        /// Экземпляр <see cref="Role"/>, соответствующий указанному типу, 
        /// или <see langword="null"/>, если роль не найдена.
        /// </returns>
        Task<Role?> GetByTypeAsync(RoleType type, CancellationToken cancellationToken = default);
    }
}