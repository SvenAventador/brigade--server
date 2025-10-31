using Brigade.Domain.Exceptions;

namespace Brigade.Domain.Entities
{
    /// <summary>
    /// Сущность для токена обновления (refresh token) пользователя.
    /// </summary>
    public class UserRefreshToken
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Значение токена обновления.
        /// </summary>
        public string RefreshToken { get; private set; } 

        /// <summary>
        /// Дата и время создания токена.
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Дата и время истечения токена.
        /// </summary>
        public DateTime ExpiresAt { get; private set; }

        /// <summary>
        /// Признак истечения срока действия токена.
        /// </summary>
        public bool IsExpires { get; private set; } 

        /// <summary>
        /// Дата и время отзыва токена (если токен был отозван).
        /// </summary>
        public DateTime? RevokedAt { get; private set; }

        /// <summary>
        /// Признак, указывающий, был ли токен отозван.
        /// </summary>
        public bool IsRevoked { get; private set; }

        #region Навигационные ключи

        /// <summary>
        /// Идентификатор пользователя, которому принадлежит токен.
        /// </summary>
        public Guid UserId { get; private set; }

        /// <summary>
        /// Навигационное свойство.
        /// </summary>
        public User? User { get; private set; }

        #endregion

        /// <summary>
        /// Создаёт новый экземпляр <see cref="UserRefreshToken"/>.
        /// </summary>
        /// <param name="userId"> Идентификатор пользователя. </param>
        /// <param name="refreshToken"> Значение токена обновления. </param>
        /// <param name="expiresAt"> Дата и время истечения токена. </param>
        /// <param name="isExpires"> "Живой ли еще" токен. </param>
        /// <param name="isRevoked"> Был ли отозван токен. </param>
        public UserRefreshToken(Guid userId,
                                string refreshToken, 
                                DateTime expiresAt,
                                bool isExpires = false,
                                bool isRevoked = false)
        {
            Guard.AgainstNullOrWhiteSpace(refreshToken, nameof(refreshToken));
            Guard.Against(userId => userId == Guid.Empty,
                          userId,
                          nameof(userId),
                          "UserId cannot be an empty GUID.");

            Id = Guid.NewGuid();
            UserId = userId;
            RefreshToken = refreshToken;
            CreatedAt = DateTime.UtcNow;
            ExpiresAt = expiresAt;
            IsExpires = isExpires;
            IsRevoked = isRevoked;
        }

        private UserRefreshToken() { }
    }
}