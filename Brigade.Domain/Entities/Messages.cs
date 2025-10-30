using Brigade.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brigade.Domain.Entities
{
    /// <summary>
    /// Сообщения внутри конкретного чата.
    /// </summary>
    public class Messages
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Идентификатор отправителя.
        /// </summary>
        public Guid SenderId { get; private set; }

        /// <summary>
        /// Идентификатор чата, к которому относится сообщение.
        /// </summary>
        public Guid ChatId { get; private set; }

        /// <summary>
        /// Текст сообщения.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Дата и время отправки сообщения.
        /// </summary>
        public DateTime SendAt { get; private set; }

        /// <summary>
        /// Признак, был ли прочитано сообщение.
        /// </summary>
        public bool IsRead { get; private set; } 

        /// <summary>
        /// Создаёт новый экземпляр <see cref="Messages"/>.
        /// </summary>
        /// <param name="senderId"> Идентификатор отправителя. </param>
        /// <param name="chatId"> Идентификатор чата. </param>
        /// <param name="text"> Текст сообщения. </param>
        /// <param name="sendAt"> Дата и время отправки. </param>
        /// <param name="isRead"> Статус прочтения (по умолчанию <c>false</c>). </param>
        public Messages(Guid senderId,
                        Guid chatId,
                        string text,
                        DateTime sendAt,
                        bool isRead = false) 
        {
            Guard.AgainstNullOrWhiteSpace(text, nameof(text));
            Guard.Against(senderId => senderId == Guid.Empty,
                          senderId,
                          nameof(senderId),
                          "SenderId cannot be an empty GUID.");
            Guard.Against(chatId => chatId == Guid.Empty,
                          chatId,
                          nameof(chatId),
                          "ChatId cannot be an empty GUID.");

            Id = Guid.NewGuid();
            SenderId = senderId;
            ChatId = chatId;
            Text = text;
            SendAt = sendAt;
            IsRead = isRead;
        }
    }
}