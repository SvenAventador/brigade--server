using Brigade.Domain.Exceptions;

namespace Brigade.Domain.Entities
{
    /// <summary>
    /// Чаты в системе.
    /// </summary>
    public class Chats
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Идентификатор заказа (не обязательно).
        /// </summary>
        public Guid? OrderId { get; private set; }

        /// <summary>
        /// Навигационное свойство.
        /// </summary>
        public Orders? Orders { get; private set; }

        /// <summary>
        /// Идентификатор первого участника чата.
        /// </summary>
        public Guid FirstParticipantId { get; private set; }

        /// <summary>
        /// Навигационное свойство.
        /// </summary>
        public User? FirstParticipant { get; private set; }

        /// <summary>
        /// Идентификатор второго участника чата.
        /// </summary>
        public Guid SecondParticipantId {  get; private set; }

        /// <summary>
        /// Навигационное свойство.
        /// </summary>
        public User? SecondParticipant { get; private set; }

        /// <summary>
        /// Создаёт новый экземпляр <see cref="Chats"/>.
        /// </summary>
        /// <param name="firstParticipantId"> Идентификатор первого участника. </param>
        /// <param name="secondParticipantId"> Идентификатор второго участника. </param>
        /// <param name="orderId"> Идентификатор заказа (не обязательно). </param>
        public Chats(Guid firstParticipantId,  
                     Guid secondParticipantId,
                     Guid? orderId = null)
        {
            Guard.Against(firstParticipantId => firstParticipantId == Guid.Empty,
                          firstParticipantId,
                          nameof(firstParticipantId),
                          "FirstParticipantId cannot be an empty GUID.");
            Guard.Against(secondParticipantId => secondParticipantId == Guid.Empty,
                          secondParticipantId,
                          nameof(secondParticipantId),
                          "SecondParticipant cannot be an empty GUID.");

            Id = Guid.NewGuid();
            OrderId = orderId;
            FirstParticipantId = firstParticipantId;
            SecondParticipant = secondParticipantId;
        }
    }
}