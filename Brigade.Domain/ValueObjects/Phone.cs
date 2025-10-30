using Brigade.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Brigade.Domain.ValueObjects
{
    /// <summary>
    /// ValueObject для мобильного телефона.
    /// </summary>
    public class Phone
    {
        /// <summary>
        /// Регулярное выражение для валидации мобильного телефона.
        /// </summary>
        private static readonly Regex PhoneRegex = new Regex(
            @"^[\+]?[0-9]{1,4}?[-\s\.]?[0-9]{3,4}[-\s\.]?[0-9]{3,4}[-\s\.]?[0-9]{3,4}$",
            RegexOptions.Compiled
        );

        /// <summary>
        /// Значение мобильного телефона.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Конструктор ValueObject мобильного телефона.
        /// </summary>
        /// <param name="phone"> Мобильный телефон. </param>
        /// <exception cref="InvalidUserDataException"> 
        /// Вызывается, если <paramref name="phone"/> имеет неверный формат или является пустым.
        /// </exception>
        public Phone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                throw new InvalidUserDataException("Phone cannot be null or empty.");
            if (!PhoneRegex.IsMatch(phone))
                throw new InvalidUserDataException("Invalid phone format.");

            Value = phone.Trim().ToLowerInvariant();
        }

        #region Переопределенные методы

        /// <summary>
        /// Преобразование мобильного телефона в строку.
        /// </summary>
        /// <returns> Мобильный телефон в виде строки. </returns>
        public override string ToString() => Value;

        /// <summary>
        /// Проверка равенства двух объектов Phone.
        /// </summary>
        /// <param name="obj"> Объект, который должен быть типа Phone. </param>
        /// <returns>
        /// true - если значения электронных почт равны,
        /// false - если значения электронных почт не равны.
        /// </returns>
        public override bool Equals(object? obj)
        {
            if (obj is Phone other)
                return Value == other.Value;

            return false;
        }

        /// <summary>
        /// Определение хэш-кода для электронной почты.
        /// </summary>
        /// <returns>
        /// true - если хэш-коды равны,
        /// false - если хэш-коды не равны.
        /// </returns>
        public override int GetHashCode() => Value.GetHashCode();

        #endregion

        #region Операторы

        /// <summary>
        /// Оператор равенства для Phone.
        /// </summary>
        /// <param name="left"> Первый параметр со значением типа Phone. </param>
        /// <param name="right"> Второй параметр со значением типа Phone. </param>
        /// <returns>
        /// true - если значения равны,
        /// false - если значения не равны.
        /// </returns>
        public static bool operator ==(Phone left, Phone right)
        {
            if (left is null && right is null)
                return true;
            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        /// <summary>
        /// Оператор неравенства для Phone.
        /// </summary>
        /// <param name="left"> Первый параметр со значением типа Phone. </param>
        /// <param name="right"> Второй параметр со значением типа Phone. </param>
        /// <returns>
        /// true - если значения равны,
        /// false - если значения не равны.
        /// </returns>
        public static bool operator !=(Phone left, Phone right) => !(left == right);

        /// <summary>
        /// Представление Phone как строки напрямую.
        /// </summary>
        /// <param name="email"> Мобильный телефон. </param>
        public static implicit operator string(Phone email) => email.Value;

        #endregion
    }
}