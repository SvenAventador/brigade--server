using Brigade.Domain.Exceptions;
using Brigade.Domain.ValueObjects;

namespace Brigade.Domain.Entities
{
    /// <summary>
    /// Тарифный план.
    /// </summary>
    public class Tariffs
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Название тарифа.
        /// </summary>
        public Name Name { get; private set; } = null!;

        /// <summary>
        /// Описание тарифа (необязательно).
        /// </summary>
        public string? Description { get; private set; }

        /// <summary>
        /// Цена тарифа.
        /// </summary>
        public decimal Price { get; private set; }

        /// <summary>
        /// Продолжительность действия тарифа в днях.
        /// </summary>
        public int DurationDays { get; private set; }

        /// <summary>
        /// Создаёт новый экземпляр <see cref="Tariffs"/>.
        /// </summary>
        /// <param name="name"> Название тарифа. </param>
        /// <param name="price"> Цена тарифа. </param>
        /// <param name="durationDays"> Продолжительность действия тарифа в днях. </param>
        /// <param name="description"> Описание тарифа (необязательно). </param>
        public Tariffs(Name name, 
                       decimal price, 
                       int durationDays, 
                       string? description = null)
        {
            Guard.AgainstNullOrWhiteSpace(name, nameof(name));
            Guard.AgainstNegative((double)price, nameof(price));
            Guard.AgainstNegative(durationDays, nameof(durationDays));

            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            DurationDays = durationDays;
        }
    }
}