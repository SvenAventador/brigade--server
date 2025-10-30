namespace Brigade.Domain.Enums
{
    /// <summary>
    /// Статусы тарифного плана.
    /// </summary>
    public enum TariffStatus
    {
        /// <summary>
        /// Активен.
        /// </summary>
        Active,

        /// <summary>
        /// Неактивен.
        /// </summary>
        Inactive,

        /// <summary>
        /// Истек.
        /// </summary>
        Expired,

        /// <summary>
        /// Отменен.
        /// </summary>
        Cancelled,

        /// <summary>
        /// Ожидает оплаты.
        /// </summary>
        PendingPayment
    }
}