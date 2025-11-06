namespace Brigade.Application.User.Command.AuthUser
{
    /// <summary>
    /// Команда для авторизации пользователя в системе.
    /// </summary>
    public class AuthUserCommand
    {
        /// <summary>
        /// Адрес электронной почты пользователя.
        /// </summary>
        public string Email { get; set; } = null!;

        /// <summary>
        /// Пароль пользователя в открытом виде.
        /// </summary>
        public string Password { get; set; } = null!;
    }
}