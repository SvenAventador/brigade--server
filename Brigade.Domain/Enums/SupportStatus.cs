using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brigade.Domain.Enums
{
    /// <summary>
    /// Перечисление статутов обращений в службу поддержки.
    /// </summary>
    public enum SupportStatus
    {
        /// <summary>
        /// Открыт.
        /// </summary>
        Open,

        /// <summary>
        /// В обработке.
        /// </summary>
        InProgress,

        /// <summary>
        /// Решен.
        /// </summary>
        Resolved,

        /// <summary>
        /// Закрыт.
        /// </summary>
        Clodes,

        /// <summary>
        /// Ожидает ответа от пользователя.
        /// </summary>
        PendingUserResponse
    }
}
