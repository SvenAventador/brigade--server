using System.Security.Claims;

namespace Brigade.Domain.Services
{
    /// <summary>
    /// Предоставляет методы для генерации, проверки и анализа JWT-токенов.
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// Генерирует JWT-токен на основе идентификатора пользователя, email и ролей.
        /// </summary>
        /// <param name="userId"> Уникальный идентификатор пользователя. </param>
        /// <param name="email"> Адрес электронной почты пользователя. </param>
        /// <param name="roles"> Список ролей пользователя. </param>
        /// <param name="claims"> Дополнительные пользовательские утверждения (claims), которые будут включены в токен. </param>
        /// <returns> Подписанный JWT-токен в виде строки. </returns>
        string GenerateToken(Guid userId, 
                             string email, 
                             IEnumerable<string> roles, 
                             IEnumerable<KeyValuePair<string, string>>? claims = null);

        #region Опциональные методы

        /// <summary>
        /// Генерирует случайный refresh-токен (обычно длинная криптографически безопасная строка).
        /// </summary>
        /// <returns> Новый refresh-токен. </returns>
        string GenerateRefreshToken();

        /// <summary>
        /// Извлекает идентификатор пользователя из указанного JWT-токена.
        /// </summary>
        /// <param name="token"> JWT-токен для анализа. </param>
        /// <returns>
        /// Идентификатор пользователя, если токен валиден и содержит соответствующий claim;
        /// иначе — <see langword="null"/>.
        /// </returns>
        Guid? GetUserIdFromToken(string token);

        /// <summary>
        /// Проверяет валидность JWT-токена и, при успехе, возвращает его утверждения.
        /// </summary>
        /// <param name="token"> JWT-токен для проверки. </param>
        /// <param name="claimsPrincipal">
        /// Объект с утверждениями из токена, если он валиден;
        /// иначе — <see langword="null"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/>, если токен действителен и подпись верна;
        /// иначе — <see langword="false"/>.
        /// </returns>
        bool ValidateToken(string token, out ClaimsPrincipal? claimsPrincipal);

        #endregion
    }
}