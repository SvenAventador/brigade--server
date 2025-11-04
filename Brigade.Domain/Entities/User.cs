using Brigade.Domain.Enums;
using Brigade.Domain.Exceptions;
using Brigade.Domain.ValueObjects;

namespace Brigade.Domain.Entities
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; private set; }

        #region Основные атрибуты

        /// <summary>
        /// Электронная почта.
        /// </summary>
        public Email Email { get; private set; }

        /// <summary>
        /// Пароль в хешированном виде.
        /// </summary>
        public string HashPassword { get; private set; }

        /// <summary>
        /// ФИО.
        /// </summary>
        public FullName FullName { get; private set; }

        /// <summary>
        /// Телефон.
        /// </summary>
        public Phone Phone { get; private set; }

        /// <summary>
        /// Предпочитаемый способ связи.
        /// </summary>
        public PreferencesContactMethod PreferencesContact { get; private set; }

        /// <summary>
        /// Дата регистрации. По умолчанию: текущая дата.
        /// </summary>
        public DateTime RegistrationDate { get; private set; }

        /// <summary>
        /// Дата последнего входа в систему.
        /// </summary>
        public DateTime LastEnter { get; private set; }

        /// <summary>
        /// Подтвержден ли аккаунт.
        /// </summary>
        public bool IsConfirmed { get; private set; }

        #endregion

        #region Необязательные атрибуты

        /// <summary>
        /// Фотография.
        /// </summary>
        public string? Photo { get; private set; }

        /// <summary>
        /// Ссылка для подтверждения аккаунта.
        /// </summary>
        public Guid? EmailConfirmationToken { get; private set; }

        #endregion

        #region Навигационные ключи

        /// <summary>
        /// Внешний ключ.
        /// </summary>
        public Guid RegionId { get; private set; }

        /// <summary>
        /// Навигационное свойство.
        /// </summary>
        public Regions? Region { get; private set; }

        /// <summary>
        /// Навигационное свойство.
        /// </summary>
        public ICollection<UserRole> UserRoles { get; private set; } = [];

        #endregion

        /// <summary>
        /// Создаёт новый экземпляр <see cref="User"/>
        /// </summary>
        /// <param name="email"> Электронная почта. </param>
        /// <param name="hashPassword"> Пароль в хешированном виде. </param>
        /// <param name="fullName"> ФИО. </param>
        /// <param name="phone"> Номер телефона. </param>
        /// <param name="regionId"> Уникальный идентификатор региона, связанного с пользователем. </param>
        /// <param name="preferencesContact"> Предпочитаемый способ связи. </param>
        public User(
            Email email,
            string hashPassword,
            FullName fullName,
            Phone phone,
            Guid regionId,
            PreferencesContactMethod preferencesContact = PreferencesContactMethod.Phone)
        {
            Guard.AgainstNull(email, nameof(email));
            Guard.AgainstNullOrWhiteSpace(hashPassword, nameof(hashPassword));
            Guard.AgainstNullOrWhiteSpace(fullName, nameof(fullName));
            Guard.AgainstNull(phone, nameof(phone));
            Guard.Against(regionId => regionId == Guid.Empty,
                          regionId,
                          nameof(regionId),
                          "RegionId cannot be an empty GUID.");

            Id = Guid.NewGuid();
            Email = email;
            HashPassword = hashPassword;
            FullName = fullName;
            Phone = phone;
            PreferencesContact = preferencesContact;
            RegionId = regionId;
            RegistrationDate = DateTime.UtcNow;
            IsConfirmed = false;
        }

        private User() { }

        #region Методы для работы с сущностью User

        /// <summary>
        /// Подтверждение аккаунта.
        /// </summary>
        /// <exception cref="EmailAlreadyConfirmedException">
        /// Вызывается, если свойство IsConfirmed имеет значение true.
        /// </exception>
        public void ConfirmEmail()
        {
            if (IsConfirmed)
                throw new EmailAlreadyConfirmedException("Email is already confirmed");

            IsConfirmed = true;
            EmailConfirmationToken = null;
        }

        /// <summary>
        /// Генерирует токен для подтверждения email и привязывает его к пользователю.
        /// Устанавливает IsConfirmed в false.
        /// </summary>
        /// <returns>Сгенерированный токен подтверждения.</returns>
        public Guid GenerateEmailConfirmationToken()
        {
            var token = Guid.NewGuid();
            
            EmailConfirmationToken = token;
            IsConfirmed = false; 

            return token;
        }

        /// <summary>
        /// Обновление времени последнего входа.
        /// </summary>
        public void UpdateLastEnter()
            => LastEnter = DateTime.UtcNow;


        /// <summary>
        /// Смена пароля.
        /// </summary>
        /// <param name="newHashPassword"> Новый пароль (в хешированном виде) </param>
        public void ChangePassword(string newHashPassword)
        {
            Guard.AgainstNullOrWhiteSpace(newHashPassword, nameof(newHashPassword));
            HashPassword = newHashPassword;
        }

        /// <summary>
        /// Смена региона.
        /// </summary>
        /// <param name="newRegionId"> Идентификатор региона. </param>
        public void ChangeRegion(Guid newRegionId)
        {
            Guard.Against(newRegionId => newRegionId == Guid.Empty,
                                      newRegionId,
                                      nameof(newRegionId),
                                      "RegionId cannot be an empty GUID.");
            RegionId = newRegionId;
        }

        /// <summary>
        /// Смена электронного адреса.
        /// </summary>
        /// <param name="newEmail"> Новый электронный адрес. </param>
        public void ChangeEmail(Email newEmail)
        {
            Guard.AgainstNull(newEmail, nameof(newEmail));
            Email = newEmail;
            IsConfirmed = false;
        }

        /// <summary>
        /// Смена ФИО.
        /// </summary>
        /// <param name="newFullName"> Новое ФИО. </param>
        public void ChangeFullName(FullName newFullName)
        {
            Guard.AgainstNull(newFullName, nameof(newFullName));
            FullName = newFullName;
        }

        /// <summary>
        /// Смена телефона.
        /// </summary>
        /// <param name="newPhone"> Новый телефонный номер. </param>
        public void ChangePhone(Phone newPhone)
        {
            Guard.AgainstNull(newPhone, nameof(newPhone));
            Phone = newPhone;
        }

        /// <summary>
        /// Смена предпочитаемого способа связи.
        /// </summary>
        /// <param name="newPreferencesContact"> Новый способ связи. </param>
        public void ChangePreferencesContact(PreferencesContactMethod newPreferencesContact)
            => PreferencesContact = newPreferencesContact; 

        #endregion
    }
}