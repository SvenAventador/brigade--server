using Brigade.Domain.Exceptions;

namespace Brigade.Domain.ValueObjects
{
    /// <summary>
    /// ValueObject, представляющий интервал дат (Start - End).
    /// </summary>
    public class ValidityPeriod
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        public ValidityPeriod(DateTime start, DateTime end)
        {
            if (end < start)
                throw new InvalidUserDataException("End date must be greater than or equal to start date.");

            Start = start;
            End = end;
        }

        #region Переопределенные методы

        /// <summary>
        /// Преобразование интервала дат в строку.
        /// </summary>
        /// <returns> Интервал дат в виде строки (например, "Start - End"). </returns>
        public override string ToString() => $"{Start:yyyy-MM-dd} - {End:yyyy-MM-dd}"; 

        /// <summary>
        /// Проверка равенства двух объектов ValidityPeriod.
        /// </summary>
        /// <param name="obj"> Объект, который должен быть типа ValidityPeriod. </param>
        /// <returns>
        /// true - если значения Start и End равны,
        /// false - если значения Start и End не равны.
        /// </returns>
        public override bool Equals(object? obj)
        {
            if (obj is ValidityPeriod other) 
                return Start == other.Start && End == other.End; 

            return false;
        }

        /// <summary>
        /// Определение хэш-кода для интервала дат.
        /// </summary>
        /// <returns> Хэш-код, вычисленный на основе Start и End. </returns>
        public override int GetHashCode() => HashCode.Combine(Start, End); 

        #endregion

        #region Операторы

        /// <summary>
        /// Оператор равенства для ValidityPeriod.
        /// </summary>
        /// <param name="left"> Первый параметр со значением типа ValidityPeriod. </param>
        /// <param name="right"> Второй параметр со значением типа ValidityPeriod. </param>
        /// <returns>
        /// true - если значения равны,
        /// false - если значения не равны.
        /// </returns>
        public static bool operator ==(ValidityPeriod left, ValidityPeriod right) 
        {
            if (left is null && right is null)
                return true;
            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        /// <summary>
        /// Оператор неравенства для ValidityPeriod.
        /// </summary>
        /// <param name="left"> Первый параметр со значением типа ValidityPeriod. </param>
        /// <param name="right"> Второй параметр со значением типа ValidityPeriod. </param>
        /// <returns>
        /// true - если значения не равны,
        /// false - если значения равны.
        /// </returns>
        public static bool operator !=(ValidityPeriod left, ValidityPeriod right) => !(left == right); 

        /// <summary>
        /// Представление ValidityPeriod как строки напрямую.
        /// </summary>
        /// <param name="period"> Интервал дат. </param>
        public static implicit operator string(ValidityPeriod period) => period.ToString(); 

        #endregion
    }
}