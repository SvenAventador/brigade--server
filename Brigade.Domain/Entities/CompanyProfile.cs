using Brigade.Domain.Exceptions;
using Brigade.Domain.ValueObjects;

namespace Brigade.Domain.Entities
{
    /// <summary>
    /// Профиль компании (юридического лица).
    /// </summary>
    public class CompanyProfile
    {
        /// <summary>
        /// Идентификатор профиля.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Юридическое наименование компании.
        /// </summary>
        public Name LegalName { get; private set; } = null!;

        /// <summary>
        /// ИНН компании.
        /// </summary>
        public INN INN { get; private set; } = null!; 

        /// <summary>
        /// Описание компании (необязательно).
        /// </summary>
        public string? Description { get; private set; }

        #region Навигационные ключи

        /// <summary>
        /// Идентификатор пользователя, которому принадлежит профиль.
        /// </summary>
        public Guid UserId { get; private set; }

        /// <summary>
        /// Навигационное свойство. 
        /// </summary>
        public User? User { get; private set; }

        #endregion

        /// <summary>
        /// Создаёт новый экземпляр <see cref="CompanyProfile"/>.
        /// </summary>
        /// <param name="legalName"> Юридическое наименование. </param>
        /// <param name="inn"> ИНН. </param>
        /// <param name="userId"> Идентификатор пользователя. </param>
        /// <param name="description"> Описание (необязательно). </param>
        public CompanyProfile(Name legalName, 
                              INN inn, 
                              Guid userId, 
                              string? description = null)
        {
            Guard.AgainstNullOrWhiteSpace(legalName, nameof(legalName));
            Guard.AgainstNull(inn, nameof(inn));
            Guard.Against(userId => userId == Guid.Empty,
                          userId,
                          nameof(userId),
                          "UserId cannot be an empty GUID.");

            Id = Guid.NewGuid(); 
            LegalName = legalName;
            INN = inn;
            UserId = userId; 
            Description = description;
        }

        private CompanyProfile() { }
    }
}