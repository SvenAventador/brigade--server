namespace Brigade.Domain.Enums
{
    /// <summary>
    /// Статусы заказа.
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// Открыт.
        /// </summary>
        Open,

        /// <summary>
        /// Назначен.
        /// </summary>
        Assigned,

        /// <summary>
        /// В работе.
        /// </summary>
        InProgress,

        /// <summary>
        /// Выполнен.
        /// </summary>
        Completed,

        /// <summary>
        /// Отменен.
        /// </summary>
        Cancelled
    }
}