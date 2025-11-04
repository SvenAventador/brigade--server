namespace Brigade.Domain.Constants
{
    /// <summary>
    /// Константные значения проекта.
    /// </summary>
    public static class Consts
    {
        /// <summary>
        /// Максимальное количество символов в имени.
        /// </summary>
        public const int MAX_NAME_LENGTH = 255;

        /// <summary>
        /// Минимальное значение рейтинга на заказ.
        /// </summary>
        public const int MIN_RATING_VALUE = 1;

        /// <summary>
        /// Максимальное значение рейтинга на заказ.
        /// </summary>
        public const int MAX_RATING_VALUE = 5;

        /// <summary>
        /// Длина для ИНН.
        /// </summary>
        public const int INN_LENGTH = 11;

        /// <summary>
        /// Максимальная длина почты.
        /// </summary>
        public const int MAX_EMAIL_LENGTH = 255;

        /// <summary>
        /// Максимальная длина телефона.
        /// </summary>
        public const int MAX_PHONE_LENGTH = 18;

        /// <summary>
        /// Минимальная длина пароля.
        /// </summary>
        public const int MIN_PASSWORD_LENGTH = 8;
    }
}