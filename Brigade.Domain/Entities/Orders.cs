using Brigade.Domain.Enums;
using Brigade.Domain.Exceptions;
using Brigade.Domain.ValueObjects;

namespace Brigade.Domain.Entities
{
    /// <summary>
    /// Заказ.
    /// </summary>
    public class Orders
    {
        /// <summary>
        /// Идентификатор заказа.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Наименование заказа.
        /// </summary>
        public Name Title { get; private set; }

        /// <summary>
        /// Описание заказа (необязательно).
        /// </summary>
        public string? Description { get; private set; }

        /// <summary>
        /// Статус заказа.
        /// </summary>
        public OrderStatus Status { get; private set; }

        /// <summary>
        /// Цена заказа.
        /// </summary>
        public decimal Price { get; private set; }

        #region Навигационные ключи

        /// <summary>
        /// Идентификатор заказчика, создавшего заказ.
        /// </summary>
        public Guid CustomerId { get; private set; }

        /// <summary>
        /// Навигационное свойство.
        /// </summary>
        public User? Customer { get; private set; }

        /// <summary>
        /// Идентификатор региона, к которому относится заказ.
        /// </summary>
        public Guid RegionId { get; private set; }

        /// <summary>
        /// Навигационное свойство.
        /// </summary>
        public Regions? Region { get; private set; }

        #endregion

        /// <summary>
        /// Создаёт новый экземпляр <see cref="Orders"/>.
        /// </summary>
        /// <param name="customerId"> Идентификатор заказчика. </param>
        /// <param name="regionId"> Идентификатор региона. </param>
        /// <param name="title"> Наименование заказа. </param>
        /// <param name="description"> Описание заказа (необязательно). </param>
        /// <param name="price"> Цена заказа. </param>
        /// <param name="status"> Статус заказа (по умолчанию <see cref="OrderStatus.Open"/>). </param>
        public Orders(Guid customerId,
                      Guid regionId,
                      Name title,
                      string? description,
                      decimal price,
                      OrderStatus status = OrderStatus.Open)
        {
            Guard.AgainstNull(title, nameof(title));
            Guard.AgainstNegative((double)price, nameof(price));
            Guard.Against(customerId => customerId == Guid.Empty,
                          customerId,
                          nameof(customerId),
                          "CustomerId cannot be an empty GUID.");
            Guard.Against(regionId => regionId == Guid.Empty,
                          regionId,
                          nameof(regionId),
                          "RegionId cannot be an empty GUID.");

            Id = Guid.NewGuid();
            CustomerId = customerId;
            RegionId = regionId;
            Title = title;
            Description = description;
            Price = price;
            Status = status;
        }

        private Orders() { }
    }
}