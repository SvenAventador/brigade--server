using Brigade.Domain.Enums;
using Brigade.Domain.Exceptions;
using Brigade.Domain.ValueObjects;

namespace Brigade.Domain.Entities
{
    /// <summary>
    /// Заявка в техническую поддержку.
    /// </summary>
    public class SupportTickets
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Идентификатор пользователя, создавшего заявку.
        /// </summary>
        public Guid UserId { get; private set; }

        /// <summary>
        /// Тема заявки.
        /// </summary>
        public Name Subject { get; private set; }

        /// <summary>
        /// Описание проблемы (необязательно).
        /// </summary>
        public string? Description { get; private set; }

        /// <summary>
        /// Статус заявки.
        /// </summary>
        public SupportStatus Status { get; private set; }

        /// <summary>
        /// Создаёт новый экземпляр <see cref="SupportTickets"/>.
        /// </summary>
        /// <param name="userId"> Идентификатор пользователя. </param>
        /// <param name="subject"> Тема заявки. </param>
        /// <param name="description"> Описание проблемы (необязательно). </param>
        /// <param name="status"> Статус обращения (по умолчанию <see cref="SupportStatus.Open"/>). </param>
        public SupportTickets(Guid userId,
                              Name subject, 
                              string? description = null,
                              SupportStatus status = SupportStatus.Open)
        {
            Guard.AgainstNullOrEmpty(subject, nameof(subject));
            Guard.Against(userId => userId == Guid.Empty,
                          userId,
                          nameof(userId),
                          "UserId cannot be an empty GUID.");

            Id = Guid.NewGuid();
            UserId = userId;
            Subject = subject;
            Description = description;
            Status = status;
        }
    }
}