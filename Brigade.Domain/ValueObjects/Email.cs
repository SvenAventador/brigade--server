using Brigade.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Brigade.Domain.ValueObjects
{
    /// <summary>
    /// ValueObject для электронной почты.
    /// </summary>
    public class Email
    {
        /// <summary>
        /// Регулярное выражение для валидации электронной почты.
        /// </summary>
        private static readonly Regex EmailRegex = new Regex(
           @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
           RegexOptions.Compiled | RegexOptions.IgnoreCase
        );

        /// <summary>
        /// Значение электронной почты.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Конструктор ValueObject электронной почты.
        /// </summary>
        /// <param name="email"> Электронная почта. </param>
        /// <exception cref="InvalidUserDataException"> 
        /// Вызывается, если <paramref name="email"/> имеет неверный формат или является пустым.
        /// </exception>
        public Email(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new InvalidUserDataException("Email cannot be null or empty.");
            if (!EmailRegex.IsMatch(email))
                throw new InvalidUserDataException("Invalid email format.");

            Value = email.Trim().ToLowerInvariant();
        }

        #region Переопределенные методы

        /// <summary>
        /// Преобразование электронной почты в строку.
        /// </summary>
        /// <returns> Электронная почта в виде строки. </returns>
        public override string ToString() => Value;

        /// <summary>
        /// Проверка равенства двух объектов Email.
        /// </summary>
        /// <param name="obj"> Объект, который должен быть типа Email. </param>
        /// <returns>
        /// true - если значения электронных почт равны,
        /// false - если значения электронных почт не равны.
        /// </returns>
        public override bool Equals(object? obj)
        {
            if (obj is Email other)
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
        /// Оператор равенства для Email.
        /// </summary>
        /// <param name="left"> Первый параметр со значением типа Email. </param>
        /// <param name="right"> Второй параметр со значением типа Email. </param>
        /// <returns>
        /// true - если значения равны,
        /// false - если значения не равны.
        /// </returns>
        public static bool operator ==(Email left, Email right)
        {
            if (left is null && right is null)
                return true;
            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        /// <summary>
        /// Оператор неравенства для Email.
        /// </summary>
        /// <param name="left"> Первый параметр со значением типа Email. </param>
        /// <param name="right"> Второй параметр со значением типа Email. </param>
        /// <returns>
        /// true - если значения равны,
        /// false - если значения не равны.
        /// </returns>
        public static bool operator !=(Email left, Email right) => !(left == right);

        /// <summary>
        /// Представление Email как строки напрямую.
        /// </summary>
        /// <param name="email"> Электронная почта. </param>
        public static implicit operator string(Email email) => email.Value;

        #endregion
    }
}