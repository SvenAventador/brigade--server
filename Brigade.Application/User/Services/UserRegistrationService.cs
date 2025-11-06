using Brigade.Application.Common;
using Brigade.Application.User.Command.RegisterUser;
using Brigade.Domain.Entities;
using Brigade.Domain.Enums;
using Brigade.Domain.Repositories;
using Brigade.Domain.Services;
using Brigade.Domain.ValueObjects;

namespace Brigade.Application.User.Services
{
    /// <summary>
    /// Сервис приложения для регистрации пользователей.
    /// </summary>
    public class UserRegistrationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ICompanyProfileRepository _companyProfileRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IPasswordHasherService _passwordHasherService;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Создаёт новый экземпляр <see cref="UserAuthService"/>.
        /// </summary>
        /// <param name="userRepository"> Репозиторий пользователей. </param>
        /// <param name="roleRepository"> Репозиторий ролей. </param>
        /// <param name="userRoleRepository"> Репозиторий связей пользователь-роль. </param>
        /// <param name="companyProfileRepository"> Репозиторий профиля компаний. </param>
        /// <param name="passwordHasherService"> Сервис хеширования паролей. </param>
        /// <param name="unitOfWork"> Единица работы для управления транзакциями. </param>
        public UserRegistrationService(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            ICompanyProfileRepository companyProfileRepository,
            IUserRoleRepository userRoleRepository,
            IPasswordHasherService passwordHasherService,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _companyProfileRepository = companyProfileRepository;
            _userRoleRepository = userRoleRepository;
            _passwordHasherService = passwordHasherService;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Асинхронно регистрирует нового пользователя.
        /// </summary>
        /// <param name="command"> Команда регистрации, содержащая данные пользователя. </param>
        /// <param name="cancellationToken"> Токен отмены операции. </param>
        /// <returns> Результат операции, содержащий идентификатор нового пользователя в случае успеха или ошибки в случае неудачи. </returns>
        public async Task<Result<Guid>> RegisterAsync(RegisterUserCommand command, CancellationToken cancellationToken = default)
        {
            var existingUser = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);
            if (existingUser != null)
                return Result<Guid>.Failure("Пользователь с таким email уже существует.");

            var voResult = await ConvertCommandToValueObjects(command, cancellationToken);
            if (!voResult.IsSuccess)
                return Result<Guid>.Failure(voResult.Errors!); 
            var (emailVo, fullNameVo, phoneVo, companyLegalNameVo, companyInnVo) = voResult.Value;

            var regionIdResult = ValidateRegionId(command.RegionId);
            if (!regionIdResult.IsSuccess)
                return Result<Guid>.Failure(regionIdResult.Errors!); 
            var regionId = regionIdResult.Value;

            var preferencesContactResult = ConvertPreferencesContact(command.PreferencesContact);
            if (!preferencesContactResult.IsSuccess)
                return Result<Guid>.Failure(preferencesContactResult.Errors!); 
            var preferencesContact = preferencesContactResult.Value;

            var userTypeResult = ConvertUserRole(command.UserRole);
            if (!userTypeResult.IsSuccess)
                return Result<Guid>.Failure(userTypeResult.Errors!); 
            var userType = userTypeResult.Value;

            var role = await _roleRepository.GetByTypeAsync(userType, cancellationToken);
            if (role == null)
                return Result<Guid>.Failure("Указанная роль не найдена.");

            var hashedPassword = _passwordHasherService.HashPassword(command.Password);

            var user = new Domain.Entities.User(emailVo, 
                                                hashedPassword, 
                                                fullNameVo,
                                                phoneVo, 
                                                regionId, 
                                                preferencesContact);

            var confirmationToken = user.GenerateEmailConfirmationToken();
            await _userRepository.AddAsync(user, cancellationToken);

            var userRole = new UserRole(user.Id, role.Id);
            await _userRoleRepository.AddAsync(userRole, cancellationToken);

            if (userType == RoleType.company && 
                companyLegalNameVo is not null && 
                companyInnVo is not null)
            {
                var companyProfile = new CompanyProfile(companyLegalNameVo, 
                                                        companyInnVo, 
                                                        user.Id, 
                                                        command.CompanyDescription);
                await _companyProfileRepository.AddAsync(companyProfile, cancellationToken);
            }

            return Result<Guid>.Success(user.Id);
        }

        #region Вспомогательные методы для регистрации

        /// <summary>
        /// Преобразует строковые данные из команды регистрации в Value Objects.
        /// </summary>
        /// <param name="command"> Команда регистрации. </param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns> Результат операции, содержащий кортеж Value Objects в случае успеха или ошибки в случае неудачи. </returns>
        private async Task<Result<(Email email, 
                                   FullName fullName, 
                                   Phone phone, 
                                   Name? companyLegalName, 
                                   INN? companyInn)>>
                     ConvertCommandToValueObjects(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var email = new Email(command.Email);
                var fullName = new FullName(command.FullName);
                var phone = new Phone(command.Phone);

                Name? companyLegalName = null;
                INN? companyInn = null;

                if (command.UserRole.Equals("Company", StringComparison.OrdinalIgnoreCase))
                {
                    if (string.IsNullOrEmpty(command.CompanyLegalName) || 
                        string.IsNullOrEmpty(command.CompanyINN))
                        return Result<(Email, 
                                       FullName, 
                                       Phone, 
                                       Name?, 
                                       INN?)>.Failure("Для типа 'Company' требуются LegalName и INN.");

                    companyLegalName = new Name(command.CompanyLegalName);
                    companyInn = new INN(command.CompanyINN);
                }

                return Result<(Email, 
                               FullName, 
                               Phone,
                               Name?, 
                               INN?)>.Success((email, fullName, phone, companyLegalName, companyInn));
            }
            catch (Domain.Exceptions.InvalidUserDataException ex)
            {
                return Result<(Email, FullName, Phone, Name?, INN?)>.Failure(ex.Message);
            }
        }

        /// <summary>
        /// Преобразует строку метода связи в перечисление <see cref="PreferencesContactMethod"/>.
        /// </summary>
        /// <param name="contactStr"> Строка метода связи. </param>
        /// <returns>
        /// Результат операции, содержащий <see cref="PreferencesContactMethod"/> в случае успеха или ошибки в случае неудачи.
        /// </returns>
        private Result<PreferencesContactMethod> ConvertPreferencesContact(string? contactStr)
        {
            if (string.IsNullOrEmpty(contactStr))
                return Result<PreferencesContactMethod>.Success(PreferencesContactMethod.Phone);

            if (Enum.TryParse<PreferencesContactMethod>(contactStr, true, out var parsedContact))
                return Result<PreferencesContactMethod>.Success(parsedContact);

            return Result<PreferencesContactMethod>.Failure("Некорректный метод связи.");
        }

        /// <summary>
        /// Преобразует строку типа пользователя в перечисление <see cref="RoleType"/>.
        /// </summary>
        /// <param name="userTypeStr"> Строка типа пользователя. </param>
        /// <returns>
        /// Результат операции, содержащий <see cref="RoleType"/> в случае успеха или ошибки в случае неудачи.
        /// </returns>
        private Result<RoleType> ConvertUserRole(string userTypeStr)
        {
            if (Enum.TryParse<RoleType>(userTypeStr, true, out var userType))
                return Result<RoleType>.Success(userType);

            return Result<RoleType>.Failure("Некорректный тип пользователя.");
        }

        /// <summary>
        /// Проверяет строку идентификатора региона на корректность.
        /// </summary>
        /// <param name="regionIdStr"> Строка идентификатора региона. </param>
        /// <returns> Результат операции, содержащий <see cref="Guid"/> региона в случае успеха или ошибки в случае неудачи. </returns>
        private Result<Guid> ValidateRegionId(string regionIdStr)
        {
            if (Guid.TryParse(regionIdStr, out var regionId) && 
                regionId != Guid.Empty)
                return Result<Guid>.Success(regionId);

            return Result<Guid>.Failure("Некорректный идентификатор региона.");
        }

        #endregion
    }
}