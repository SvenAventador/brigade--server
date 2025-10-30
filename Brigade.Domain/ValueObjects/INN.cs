using Brigade.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Brigade.Domain.ValueObjects
{
    /// <summary>
    /// ValueObject для ИНН.
    /// </summary>
    public class INN
    {
        /// <summary>
        /// Регулярное выражение для валидации ИНН.
        /// </summary>
        private static readonly Regex INNRegex = new Regex(
           @"^\d{11}$",
           RegexOptions.Compiled | RegexOptions.IgnoreCase
        );

        /// <summary>
        /// Значение ИНН.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Конструктор ValueObject ИНН.
        /// </summary>
        /// <param name="inn"> ИНН. </param>
        /// <exception cref="InvalidUserDataException"> 
        /// Вызывается, если <paramref name="inn"/> имеет неверный формат или является пустым.
        /// </exception>
        public INN(string inn)
        {
            if (string.IsNullOrWhiteSpace(inn))
                throw new InvalidUserDataException("INN cannot be null or empty.");

            if (!INNRegex.IsMatch(inn))
                throw new InvalidUserDataException("INN must contain exactly 11 digits.");

            Value = inn.Trim();
        }

        #region Переопределенные методы

        /// <summary>
        /// Преобразование ИНН в строку.
        /// </summary>
        /// <returns> ИНН в виде строки. </returns>
        public override string ToString() => Value;

        /// <summary>
        /// Проверка равенства двух объектов INN.
        /// </summary>
        /// <param name="obj"> Объект, который должен быть типа INN. </param>
        /// <returns>
        /// true - если значения ИНН равны,
        /// false - если значения ИНН не равны.
        /// </returns>
        public override bool Equals(object? obj)
        {
            if (obj is INN other)
                return Value == other.Value;

            return false;
        }

        /// <summary>
        /// Определение хэш-кода для ИНН.
        /// </summary>
        /// <returns>
        /// true - если хэш-коды равны,
        /// false - если хэш-коды не равны.
        /// </returns>
        public override int GetHashCode() => Value.GetHashCode();

        #endregion

        #region Операторы

        /// <summary>
        /// Оператор равенства для INN.
        /// </summary>
        /// <param name="left"> Первый параметр со значением типа INN. </param>
        /// <param name="right"> Второй параметр со значением типа INN. </param>
        /// <returns>
        /// true - если значения равны,
        /// false - если значения не равны.
        /// </returns>
        public static bool operator ==(INN left, INN right)
        {
            if (left is null && right is null)
                return true;
            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        /// <summary>
        /// Оператор неравенства для INN.
        /// </summary>
        /// <param name="left"> Первый параметр со значением типа INN. </param>
        /// <param name="right"> Второй параметр со значением типа INN. </param>
        /// <returns>
        /// true - если значения равны,
        /// false - если значения не равны.
        /// </returns>
        public static bool operator !=(INN left, INN right) => !(left == right);

        /// <summary>
        /// Представление INN как строки напрямую.
        /// </summary>
        /// <param name="inn"> ИНН. </param>
        public static implicit operator string(INN inn) => inn.Value;

        #endregion
    }
}