using Brigade.Domain.Exceptions;
using Brigade.Domain.ValueObjects;

namespace Brigade.Domain.Entities
{
    /// <summary>
    /// Отзывы.
    /// </summary>
    public class Reviews
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Рейтинг, присвоенный в отзыве.
        /// </summary>
        public Rating Rating { get; private set; }

        /// <summary>
        /// Комментарий, оставленный в отзыве.
        /// </summary>
        public string Comment { get; private set; }

        #region Навигационные ключи

        /// <summary>
        /// Идентификатор пользователя, оставившего отзыв (автор).
        /// </summary>
        public Guid AuthorId { get; private set; }

        public User? Author { get; private set; }

        /// <summary>
        /// Идентификатор пользователя, которому оставлен отзыв (цель).
        /// </summary>
        public Guid TargetId { get; private set; }

        public User? Target { get; private set; }

        /// <summary>
        /// Идентификатор заказа, к которому относится отзыв.
        /// </summary>
        public Guid OrderId { get; private set; }

        public Orders? Order { get; private set; }

        #endregion

        /// <summary>
        /// Создаёт новый экземпляр <see cref="Reviews"/>.
        /// </summary>
        /// <param name="authorId"> Идентификатор автора отзыва. </param>
        /// <param name="targetId"> Идентификатор пользователя, которому оставлен отзыв. </param>
        /// <param name="orderId"> Идентификатор заказа. </param>
        /// <param name="rating"> Рейтинг. </param>
        /// <param name="comment"> Комментарий. </param>
        public Reviews(Guid authorId, 
                       Guid targetId, 
                       Guid orderId, 
                       Rating rating, 
                       string comment)
        {
            Guard.AgainstNull(comment, nameof(comment));
            Guard.AgainstNull(rating, nameof(rating));
            Guard.Against(authorId => authorId == Guid.Empty,
                          authorId,
                          nameof(authorId),
                          "AuthorId cannot be an empty GUID.");
            Guard.Against(targetId => targetId == Guid.Empty,
                          targetId,
                          nameof(targetId),
                          "TargetId cannot be an empty GUID.");
            Guard.Against(orderId => orderId == Guid.Empty,
                          orderId,
                          nameof(orderId),
                          "OrderId cannot be an empty GUID.");

            Id = Guid.NewGuid();
            AuthorId = authorId;
            TargetId = targetId;
            OrderId = orderId;
            Rating = rating;
            Comment = comment;
        }

        private Reviews() { }
    }
}