using Brigade.Domain.Enums;
using Brigade.Domain.Exceptions;
using Brigade.Domain.ValueObjects;

namespace Brigade.Domain.Entities
{
    /// <summary>
    /// Подписка на тариф.
    /// </summary>
    public class UserTariffs
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Идентификатор пользователя, оформившего подписку.
        /// </summary>
        public Guid UserId { get; private set; }

        /// <summary>
        /// Идентификатор тарифа.
        /// </summary>
        public Guid TariffId { get; private set; }

        /// <summary>
        /// Сумма оплаты за тариф.
        /// </summary>
        public decimal Amount { get; private set; }

        /// <summary>
        /// Статус подписки.
        /// </summary>
        public TariffStatus Status { get; private set; }

        /// <summary>
        /// Период действия подписки.
        /// </summary>
        public ValidityPeriod ValidityPeriod { get; private set; } 

        /// <summary>
        /// Создаёт новый экземпляр <see cref="UserTariffs"/>.
        /// </summary>
        /// <param name="userId"> Идентификатор пользователя. </param>
        /// <param name="tariffId"> Идентификатор тарифа. </param>
        /// <param name="amount"> Сумма оплаты. </param>
        /// <param name="startDate"> Дата начала действия подписки. </param>
        /// <param name="endDate"> Дата окончания действия подписки. </param>
        /// <param name="status"> Статус подписки (по умолчанию <see cref="TariffStatus.Active"/>). </param>
        public UserTariffs(Guid userId, 
                           Guid tariffId, 
                           decimal amount, 
                           ValidityPeriod validityPeriod,
                           TariffStatus status = TariffStatus.Active)
        {
            Guard.AgainstNull(validityPeriod, nameof(validityPeriod));
            Guard.AgainstNegative((double)amount, nameof(amount));
            Guard.Against(userId => userId == Guid.Empty,
                          userId,
                          nameof(userId),
                          "UserId cannot be an empty GUID.");
            Guard.Against(tariffId => tariffId == Guid.Empty,
                          tariffId,
                          nameof(tariffId),
                          "TariffId cannot be an empty GUID.");

            Id = Guid.NewGuid();
            UserId = userId;
            TariffId = tariffId;
            Amount = amount;
            ValidityPeriod = validityPeriod;
            Status = status; 
        }
    }
}