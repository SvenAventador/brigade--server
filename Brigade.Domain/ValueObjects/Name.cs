using Brigade.Domain.Constants;
using Brigade.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Brigade.Domain.ValueObjects
{
    /// <summary>
    /// ValueObject для электронной почты.
    /// </summary>
    public class Name
    {
        /// <summary>
        /// Значение наименования.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Конструктор ValueObject наименования.
        /// </summary>
        /// <param name="name"> Наименование. </param>
        /// <exception cref="InvalidUserDataException"> 
        /// Вызывается, если <paramref name="name"/> имеет неверный формат или является пустым.
        /// </exception>
        public Name(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidUserDataException("Name cannot be null or whitespace.");

            if (value.Length > Consts.MAX_NAME_LENGTH)
                throw new InvalidUserDataException($"Name length cannot exceed {Consts.MAX_NAME_LENGTH} characters.");

            Value = value.Trim(); // Нормализуем (убираем лишние пробелы)
        }

        #region Переопределенные методы

        /// <summary>
        /// Преобразование наименования в строку.
        /// </summary>
        /// <returns> Наименование в виде строки. </returns>
        public override string ToString() => Value;

        /// <summary>
        /// Проверка равенства двух объектов Name.
        /// </summary>
        /// <param name="obj"> Объект, который должен быть типа Name. </param>
        /// <returns>
        /// true - если значения наименований равны,
        /// false - если значения наименований не равны.
        /// </returns>
        public override bool Equals(object? obj)
        {
            if (obj is Name other)
                return Value == other.Value;

            return false;
        }

        /// <summary>
        /// Определение хэш-кода для наименования.
        /// </summary>
        /// <returns>
        /// true - если хэш-коды равны,
        /// false - если хэш-коды не равны.
        /// </returns>
        public override int GetHashCode() => Value.GetHashCode();

        #endregion

        #region Операторы

        /// <summary>
        /// Оператор равенства для Name.
        /// </summary>
        /// <param name="left"> Первый параметр со значением типа Name. </param>
        /// <param name="right"> Второй параметр со значением типа Name. </param>
        /// <returns>
        /// true - если значения равны,
        /// false - если значения не равны.
        /// </returns>
        public static bool operator ==(Name left, Name right)
        {
            if (left is null && right is null)
                return true;
            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        /// <summary>
        /// Оператор неравенства для Name.
        /// </summary>
        /// <param name="left"> Первый параметр со значением типа Name. </param>
        /// <param name="right"> Второй параметр со значением типа Name. </param>
        /// <returns>
        /// true - если значения равны,
        /// false - если значения не равны.
        /// </returns>
        public static bool operator !=(Name left, Name right) => !(left == right);

        /// <summary>
        /// Представление Name как строки напрямую.
        /// </summary>
        /// <param name="email"> Наименование. </param>
        public static implicit operator string(Name email) => email.Value;

        #endregion
    }
}