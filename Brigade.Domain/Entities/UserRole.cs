using Brigade.Domain.Exceptions;

namespace Brigade.Domain.Entities
{
    /// <summary>
    /// Связь между пользователем и ролью в системе.
    /// </summary>
    public class UserRole
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid UserId { get; private set; }

        /// <summary>
        /// Идентификатор роли.
        /// </summary>
        public Guid RoleId { get; private set; }

        /// <summary>
        /// Проверка активности роли.
        /// </summary>
        public bool IsActive { get; private set; } = true;

        /// <summary>
        /// Создаёт новый экземпляр <see cref="UserRole"/>
        /// </summary>
        /// <param name="userId"> Идентификатор пользователя. </param>
        /// <param name="roleId"> Идентификатор роли. </param>
        public UserRole(Guid userId, Guid roleId)
        {
            Guard.Against(userId => userId == Guid.Empty,
                          userId,
                          nameof(userId),
                          "UserId cannot be an empty GUID.");
            Guard.Against(roleId => roleId == Guid.Empty,
                          roleId,
                          nameof(roleId),
                          "RoleId cannot be an empty GUID.");

            UserId = userId;
            RoleId = roleId;
        }

        /// <summary>
        /// "Активация" роли для пользователя.
        /// </summary>
        public void Activate()
            => IsActive = true;

        /// <summary>
        /// "Деактивация" роли для пользователя.
        /// </summary>
        public void Deactivate()
            => IsActive = false;
    }
}