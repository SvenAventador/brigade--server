using Brigade.Domain.Entities;

namespace Brigade.Domain.Repositories
{
    /// <summary>
    /// Интерфейс репозитория для работы с сущностью <see cref="UserRefreshToken"/>.
    /// </summary>
    public interface IUserRefreshTokenRepository
    {
        /// <summary>
        /// Асинхронно добавляет новый токен обновления в хранилище.
        /// </summary>
        /// <param name="refreshToken"> Сущность токена обновления. </param>
        /// <param name="cancellationToken"> Токен отмены операции. </param>
        Task AddAsync(UserRefreshToken refreshToken, CancellationToken cancellationToken = default);

        /// <summary>
        /// Асинхронно находит токен обновления по его значению.
        /// </summary>
        /// <param name="token"> Значение токена обновления. </param>
        /// <param name="cancellationToken"> Токен отмены операции. </param>
        /// <returns>Найденный токен обновления или null, если не найден.</returns>
        Task<UserRefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);

        /// <summary>
        /// Асинхронно обновляет существующий токен обновления (например, для установки признака отзыва).
        /// </summary>
        /// <param name="refreshToken"> Сущность токена обновления с новым состоянием. </param>
        /// <param name="cancellationToken"> Токен отмены операции. </param>
        Task UpdateAsync(UserRefreshToken refreshToken, CancellationToken cancellationToken = default);

        /// <summary>
        /// Асинхронно отзывает токен (например, при выходе пользователя из системы).
        /// </summary>
        /// <param name="token"> Значения токена обновления. </param>
        /// <param name="cancellationToken"> Токен отмены операции. </param>
        Task RevokeByTokenAsync(string token, CancellationToken cancellationToken = default);

        /// <summary>
        /// Асинхронно находит токен обновления по идентификатору пользователя.
        /// </summary>
        /// <param name="userId"> Идентификатор пользователя. </param>
        /// <param name="cancellationToken"> Токен отмены операции. </param>
        /// <returns>Найденный токен обновления или null, если не найден.</returns>
        Task<UserRefreshToken?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}