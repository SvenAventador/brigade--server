namespace Brigade.Domain.Services
{
    /// <summary>
    /// Предоставляет методы для безопасного хеширования паролей и их верификации.
    /// </summary>
    public interface IPasswordHasherService
    {
        /// <summary>
        /// Создаёт криптографически безопасный хеш указанного пароля.
        /// </summary>
        /// <param name="password"> Пароль в виде открытого текста. </param>
        /// <returns> Хешированное представление пароля, включая соль и параметры алгоритма. </returns>
        /// <exception cref="ArgumentException">
        /// Выбрасывается, если пароль равен <see langword="null"/> или пуст.
        /// </exception>
        string HashPassword(string password);

        /// <summary>
        /// Проверяет, соответствует ли указанный пароль в открытом виде заданному хешу.
        /// </summary>
        /// <param name="password"> Пароль в виде открытого текста. </param>
        /// <param name="hashedPassword"> Хешированный пароль, с которым выполняется сравнение. </param>
        /// <returns>
        /// <see langword="true"/>, если пароль корректен;
        /// иначе — <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Выбрасывается, если любой из аргументов равен <see langword="null"/> или пуст.
        /// </exception>
        bool VerifyPassword(string password, string hashedPassword);
    }
}