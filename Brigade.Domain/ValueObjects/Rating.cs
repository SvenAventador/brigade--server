using Brigade.Domain.Constants;
using Brigade.Domain.Exceptions;

namespace Brigade.Domain.ValueObjects
{
    /// <summary>
    /// ValueObject, представляющий рейтинг.
    /// </summary>
    public class Rating
    {
        /// <summary>
        /// Значение рейтинга.
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// Создаёт новый экземпляр <see cref="Rating"/>.
        /// </summary>
        /// <param name="value"> Значение рейтинга. Должно быть в пределах от <see cref="Consts.MIN_RATING_VALUE"/> 
        /// до <see cref="Consts.MAX_RATING_VALUE"/>. </param>
        /// <exception cref="InvalidUserDataException">
        /// Выбрасывается, если <paramref name="value"/> 
        /// выходит за допустимые границы.
        /// </exception>
        public Rating(int value)
        {
            if (value < Consts.MIN_RATING_VALUE || 
                value > Consts.MAX_RATING_VALUE)
                throw new InvalidUserDataException($"Rating must be between {Consts.MIN_RATING_VALUE} and {Consts.MAX_RATING_VALUE}.");

            Value = value;
        }

        #region Операторы преобразования

        /// <summary>
        /// Явное преобразование из <see cref="int"/> в <see cref="Rating"/>.
        /// </summary>
        /// <param name="value">Целочисленное значение рейтинга.</param>
        /// <returns>Новый экземпляр <see cref="Rating"/>.</returns>
        public static explicit operator Rating(int value) => new(value);

        /// <summary>
        /// Неявное преобразование из <see cref="Rating"/> в <see cref="int"/>.
        /// </summary>
        /// <param name="rating">Экземпляр <see cref="Rating"/>.</param>
        /// <returns>Значение рейтинга как <see cref="int"/>.</returns>
        public static implicit operator int(Rating rating) => rating.Value;

        #endregion

        #region Переопределённые методы

        /// <summary>
        /// Возвращает строковое представление рейтинга.
        /// </summary>
        /// <returns>Строка, содержащая числовое значение рейтинга.</returns>
        public override string ToString() => Value.ToString();

        /// <summary>
        /// Проверяет равенство двух экземпляров <see cref="Rating"/>.
        /// </summary>
        /// <param name="obj"> Объект для сравнения. </param>
        /// <returns><c>true</c>, если значения равны, иначе <c>false</c>.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is Rating other)
                return Value == other.Value;

            return false;
        }

        /// <summary>
        /// Возвращает хэш-код для экземпляра <see cref="Rating"/>.
        /// </summary>
        /// <returns> Хэш-код, основанный на значении <see cref="Value"/>. </returns>
        public override int GetHashCode() => Value.GetHashCode();

        #endregion

        #region Операторы равенства

        /// <summary>
        /// Проверяет равенство двух экземпляров <see cref="Rating"/>.
        /// </summary>
        /// <param name="left"> Левый операнд. </param>
        /// <param name="right"> Правый операнд. </param>
        /// <returns><c>true</c>, если значения равны, иначе <c>false</c>.</returns>
        public static bool operator ==(Rating left, Rating right)
        {
            if (left is null && right is null)
                return true;
            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        /// <summary>
        /// Проверяет неравенство двух экземпляров <see cref="Rating"/>.
        /// </summary>
        /// <param name="left"> Левый операнд. </param>
        /// <param name="right"> Правый операнд. </param>
        /// <returns><c>true</c>, если значения не равны, иначе <c>false</c>.</returns>
        public static bool operator !=(Rating left, Rating right) => !(left == right);

        #endregion
    }
}
