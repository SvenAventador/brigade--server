using Brigade.Domain.Enums;

namespace Brigade.Domain.Entities
{
    /// <summary>
    /// Системные роли пользователей.
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Выбор роли.
        /// </summary>
        public RoleType Type { get; private set; }

        /// <summary>
        /// Создаёт новый экземпляр <see cref="Role"/>
        /// </summary>
        /// <param name="type"> Тип роли, назначаемой новому экземпляру </param>
        public Role(RoleType type) 
        {
            Id = Guid.NewGuid();
            Type = type;
        }

        private Role() { }
    }
}