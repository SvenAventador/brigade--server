using Brigade.Domain.Enums;
using Brigade.Domain.Exceptions;

namespace Brigade.Domain.Entities
{
    /// <summary>
    /// Заявки исполнителя на заказ.
    /// </summary>
    public class OrderApplication
    {
        /// <summary>
        /// Идентификатор заказа.
        /// </summary>
        public Guid OrderId { get; private set; }

        /// <summary>
        /// Идентификатор исполнителя.
        /// </summary>
        public Guid PerformerId { get; private set; }

        /// <summary>
        /// Статус заявки.
        /// </summary>
        public OrderApplicationStatus Status { get; private set; }

        /// <summary>
        /// Создаёт новый экземпляр <see cref="OrderApplication"/>.
        /// </summary>
        /// <param name="orderId"> Идентификатор заказа. </param>
        /// <param name="performerId"> Идентификатор исполнителя. </param>
        /// <param name="status"> Статус заявки (по умолчанию <see cref="OrderApplicationStatus.Pending"/>). </param>
        public OrderApplication(Guid orderId, 
                                Guid performerId, 
                                OrderApplicationStatus status = OrderApplicationStatus.Pending)
        {
            Guard.Against(orderId => orderId == Guid.Empty,
                          orderId,
                          nameof(orderId),
                          "OrderId cannot be an empty GUID.");
            Guard.Against(performerId => performerId == Guid.Empty,
                          performerId,
                          nameof(performerId),
                          "PerformerId cannot be an empty GUID.");

            OrderId = orderId;
            PerformerId = performerId;
            Status = status;
        }
    }
}