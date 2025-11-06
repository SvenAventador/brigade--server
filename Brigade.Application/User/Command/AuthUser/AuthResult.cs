namespace Brigade.Application.User.Command.AuthUser
{
    /// <summary>
    /// Результат аутентификации пользователя.
    /// </summary>
    public class AuthResult
    {
        /// <summary>
        /// Идентификатор аутентифицированного пользователя.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Токен доступа (JWT), используемый для аутентификации последующих запросов.
        /// </summary>
        public string AccessToken { get; set; } = null!;

        /// <summary>
        /// Токен обновления, используемый для получения нового Access токена.
        /// </summary>
        public string RefreshToken { get; set; } = null!;

        /// <summary>
        /// Время истечения действия Access токена.
        /// </summary>
        public DateTime AccessTokenExpiry { get; set; }
    }
}
