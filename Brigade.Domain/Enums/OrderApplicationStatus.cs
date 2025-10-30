using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brigade.Domain.Enums
{
    /// <summary>
    /// Статусы заявок исполнителя.
    /// </summary>
    public enum OrderApplicationStatus
    {
        /// <summary>
        /// В ожидании.
        /// </summary>
        Pending,

        /// <summary>
        /// Принята.
        /// </summary>
        Accepted,

        /// <summary>
        /// Отклонена.
        /// </summary>
        Rejected,

        /// <summary>
        /// Отменена.
        /// </summary>
        Cancelled
    }
}