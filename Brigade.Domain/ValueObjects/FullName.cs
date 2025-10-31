using Brigade.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Brigade.Domain.ValueObjects
{
    /// <summary>
    /// ValueObject для ФИО.
    /// </summary>
    public class FullName
    {
        /// <summary>
        /// Регулярное выражение для валидации ФИО.
        /// </summary>
        private static readonly Regex FullNameRegex = new Regex(
           @"^[a-zA-Zа-яА-ЯёЁ\s]+$",
           RegexOptions.Compiled | RegexOptions.IgnoreCase
        );

        /// <summary>
        /// Значение ФИО.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Конструктор ValueObject ФИО.
        /// </summary>
        /// <param name="email"> ФИО. </param>
        /// <exception cref="InvalidUserDataException"> 
        /// Вызывается, если <paramref name="fullName"/> имеет неверный формат или является пустым.
        /// </exception>
        public FullName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new InvalidUserDataException("FullName cannot be null or empty.");
            if (!FullNameRegex.IsMatch(fullName))
                throw new InvalidUserDataException("Invalid email format.");
            if (fullName.Trim().Split(' ').Length < 2)
                throw new InvalidUserDataException("FullName must contain at least first name and last name.");

            Value = fullName.Trim();
        }

        private FullName() { }

        #region Переопределенные методы

        /// <summary>
        /// Преобразование ФИО в строку.
        /// </summary>
        /// <returns> Электронная почта в виде строки. </returns>
        public override string ToString() => Value;

        /// <summary>
        /// Проверка равенства двух объектов FullName.
        /// </summary>
        /// <param name="obj"> Объект, который должен быть типа FullName. </param>
        /// <returns>
        /// true - если значения ФИО равны,
        /// false - если значения ФИО не равны.
        /// </returns>
        public override bool Equals(object? obj)
        {
            if (obj is FullName other)
                return Value == other.Value;

            return false;
        }

        /// <summary>
        /// Определение хэш-кода для ФИО.
        /// </summary>
        /// <returns>
        /// true - если хэш-коды равны,
        /// false - если хэш-коды не равны.
        /// </returns>
        public override int GetHashCode() => Value.GetHashCode();

        #endregion

        #region Операторы

        /// <summary>
        /// Оператор равенства для FullName.
        /// </summary>
        /// <param name="left"> Первый параметр со значением типа FullName. </param>
        /// <param name="right"> Второй параметр со значением типа FullName. </param>
        /// <returns>
        /// true - если значения равны,
        /// false - если значения не равны.
        /// </returns>
        public static bool operator ==(FullName left, FullName right)
        {
            if (left is null && right is null)
                return true;
            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        /// <summary>
        /// Оператор неравенства для FullName.
        /// </summary>
        /// <param name="left"> Первый параметр со значением типа FullName. </param>
        /// <param name="right"> Второй параметр со значением типа FullName. </param>
        /// <returns>
        /// true - если значения равны,
        /// false - если значения не равны.
        /// </returns>
        public static bool operator !=(FullName left, FullName right) => !(left == right);

        /// <summary>
        /// Представление FullName как строки напрямую.
        /// </summary>
        /// <param name="email"> Электронная почта. </param>
        public static implicit operator string(FullName email) => email.Value;

        #endregion
    }
}