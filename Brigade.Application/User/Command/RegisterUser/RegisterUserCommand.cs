namespace Brigade.Application.User.Command.RegisterUser
{
    /// <summary>
    /// Команда для регистрации нового пользователя в системе.
    /// Содержит как общие данные пользователя, так и специфичные для типа пользователя (физическое/юридическое лицо).
    /// </summary>
    public class RegisterUserCommand
    {
        /// <summary>
        /// Адрес электронной почты пользователя.=
        /// </summary>
        public string Email { get; set; } = null!;

        /// <summary>
        /// Пароль пользователя в открытом виде.
        /// </summary>
        public string Password { get; set; } = null!;

        /// <summary>
        /// Полное имя пользователя.
        /// </summary>
        public string FullName { get; set; } = null!;

        /// <summary>
        /// Номер телефона пользователя. 
        /// </summary>
        public string Phone { get; set; } = null!;

        /// <summary>
        /// Идентификатор региона.
        /// </summary>
        public string RegionId { get; set; } = null!;

        /// <summary>
        /// Предпочтительный способ связи с пользователем.
        /// Может быть <see langword="null"/> — тогда используется значение по умолчанию.
        /// </summary>
        public string? PreferencesContact { get; set; }

        /// <summary>
        /// Тип пользователя.
        /// Определяет, какие дополнительные поля (например, ИНН) являются обязательными.
        /// </summary>
        public string UserRole { get; set; } = null!;

        /// <summary>
        /// Полное юридическое название компании. Обязательно, если <see cref="UserRole"/> — "Company".
        /// </summary>
        public string? CompanyLegalName { get; set; }

        /// <summary>
        /// ИНН компании. Обязательно и должно быть валидным, если <see cref="UserRole"/> — "Company".
        /// </summary>
        public string? CompanyINN { get; set; }

        /// <summary>
        /// Описание деятельности компании (опционально).
        /// </summary>
        public string? CompanyDescription { get; set; }
    }
}