using Brigade.Domain.Entities;

namespace Brigade.Domain.Repositories
{
    /// <summary>
    /// Предоставляет методы для доступа к данным пользователей в хранилище.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Асинхронно получает пользователя по уникальному идентификатору.
        /// </summary>
        /// <param name="id"> Уникальный идентификатор пользователя. </param>
        /// <param name="cancellationToken"> Токен отмены операции. </param>
        /// <returns>
        /// Экземпляр <see cref="User"/>, если пользователь найден; 
        /// иначе — <see langword="null"/>.
        /// </returns>
        Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Асинхронно получает пользователя по адресу электронной почты.
        /// </summary>
        /// <param name="email"> Адрес электронной почты пользователя. </param>
        /// <param name="cancellationToken"> Токен отмены операции. </param>
        /// <returns>
        /// Экземпляр <see cref="User"/>, если пользователь с указанным email найден; 
        /// иначе — <see langword="null"/>.
        /// </returns>
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

        /// <summary>
        /// Асинхронно добавляет нового пользователя в хранилище.
        /// </summary>
        /// <param name="user"> Объект пользователя для добавления. </param>
        /// <param name="cancellationToken"> Токен отмены операции. </param>
        /// <returns> Задача, представляющая асинхронную операцию. </returns>
        Task AddAsync(User user, CancellationToken cancellationToken = default);
    }
}