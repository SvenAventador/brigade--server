using Brigade.Application.Common;
using Brigade.Application.User.Command.AuthUser;
using Brigade.Domain.Constants;
using Brigade.Domain.Entities;
using Brigade.Domain.Repositories;
using Brigade.Domain.Services;

namespace Brigade.Application.User.Services
{
    /// <summary>
    /// Сервис приложения для аутентификации (логина), обновления токенов и выхода (логаута) пользователей.
    /// </summary>
    public class UserAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IJwtService _jwtService;
        private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;
        private readonly IPasswordHasherService _passwordHasherService;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Создаёт новый экземпляр <see cref="UserAuthService"/>.
        /// </summary>
        /// <param name="userRepository"> Репозиторий пользователей. </param>
        /// <param name="roleRepository"> Репозиторий ролей. </param>
        /// <param name="userRoleRepository"> Репозиторий связей пользователь-роль. </param>
        /// <param name="passwordHasherService"> Сервис хеширования паролей. </param>
        /// <param name="userRefreshTokenRepository"> Репозиторий refresh токенов. </param>
        /// <param name="jwtService"> Сервис генерации и валидации JWT. </param>
        /// <param name="unitOfWork"> Единица работы для управления транзакциями. </param>
        public UserAuthService(IUserRepository userRepository,
                               IRoleRepository roleRepository,
                               IUserRoleRepository userRoleRepository,
                               IPasswordHasherService passwordHasherService,
                               IUserRefreshTokenRepository userRefreshTokenRepository,
                               IJwtService jwtService,
                               IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _passwordHasherService = passwordHasherService;
            _userRefreshTokenRepository = userRefreshTokenRepository;
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Асинхронно аутентифицирует пользователя по email и паролю.
        /// </summary>
        /// <param name="command"> Команда аутентификации, содержащая email и пароль. </param>
        /// <param name="cancellationToken"> Токен отмены операции. </param>
        /// <returns> Результат операции, содержащий <see cref="AuthResult"/> в случае успеха или ошибки в случае неудачи. </returns>
        public async Task<Result<AuthResult>> AuthAsync(AuthUserCommand command, CancellationToken cancellationToken = default)
        {
            var existingUser = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);
            if (existingUser is null)
                return Result<AuthResult>.Failure("Пользователь с таким email не найден.");

            if (!_passwordHasherService.VerifyPassword(command.Password, existingUser.HashPassword))
                return Result<AuthResult>.Failure("Пароли не совпадают!");

            existingUser.UpdateLastEnter();

            var userRoles = await GetUserRolesAsync(existingUser.Id, cancellationToken);

            var accessToken = _jwtService.GenerateToken(existingUser.Id, existingUser.Email.Value, userRoles);
            var refreshTokenValue = _jwtService.GenerateRefreshToken();
            var refreshTokenExpiry = DateTime.UtcNow.AddDays(Consts.REFRESH_TOKEN_EXPIRES);

            var refreshTokenEntity = new UserRefreshToken(existingUser.Id, refreshTokenValue, refreshTokenExpiry);
            await _userRefreshTokenRepository.AddAsync(refreshTokenEntity, cancellationToken);

            var authResult = new AuthResult
            {
                UserId = existingUser.Id,
                AccessToken = accessToken,
                RefreshToken = refreshTokenValue,
                AccessTokenExpiry = DateTime.UtcNow.AddMinutes(30)
            };

            return Result<AuthResult>.Success(authResult);
        }

        /// <summary>
        /// Асинхронно обновляет токены доступа на основе refresh токена.
        /// </summary>
        /// <param name="refreshTokenValue"> Значение текущего refresh токена. </param>
        /// <param name="cancellationToken"> Токен отмены операции. </param>
        /// <returns> 
        /// Результат операции, содержащий <see cref="AuthResult"/> с новыми токенами в случае успеха или ошибки в случае неудачи. 
        /// </returns>
        public async Task<Result<AuthResult>> RefreshTokenAsync(string refreshTokenValue, CancellationToken cancellationToken = default)
        {
            var refreshToken = await _userRefreshTokenRepository.GetByTokenAsync(refreshTokenValue, cancellationToken);
            if (refreshToken == null)
                return Result<AuthResult>.Failure("Refresh токен недействителен.");

            if (refreshToken.IsRevoked)
                return Result<AuthResult>.Failure("Refresh токен был отозван.");

            if (refreshToken.IsExpired())
                return Result<AuthResult>.Failure("Срок действия refresh токена истёк.");

            var user = await _userRepository.GetByIdAsync(refreshToken.UserId, cancellationToken);
            if (user == null)
                return Result<AuthResult>.Failure("Пользователь, связанный с токеном, не найден.");

            var userRoles = await GetUserRolesAsync(user.Id, cancellationToken);

            var newAccessToken = _jwtService.GenerateToken(user.Id, user.Email.Value, userRoles);
            var newRefreshTokenValue = _jwtService.GenerateRefreshToken();
            var newRefreshTokenExpiry = DateTime.UtcNow.AddDays(Consts.REFRESH_TOKEN_EXPIRES);

            refreshToken.RevokeToken();
            await _userRefreshTokenRepository.UpdateAsync(refreshToken, cancellationToken);

            var newRefreshTokenEntity = new UserRefreshToken(
                user.Id, newRefreshTokenValue, newRefreshTokenExpiry
            );

            await _userRefreshTokenRepository.AddAsync(newRefreshTokenEntity, cancellationToken);

            var authResult = new AuthResult
            {
                UserId = user.Id,
                AccessToken = newAccessToken,
                RefreshToken = newRefreshTokenValue,
                AccessTokenExpiry = DateTime.UtcNow.AddMinutes(30)
            };

            return Result<AuthResult>.Success(authResult);
        }

        /// <summary>
        /// Асинхронно отзывает refresh токен (логаут).
        /// </summary>
        /// <param name="refreshTokenValue"> Значение refresh токена, подлежащего отзыву. </param>
        /// <param name="cancellationToken"> Токен отмены операции. </param>
        /// <returns> Результат операции. Успех, если токен был успешно отозван или уже недействителен. </returns>
        public async Task<Result<Unit>> LogoutAsync(string refreshTokenValue, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(refreshTokenValue))
                return Result<Unit>.Failure("Refresh token is required for logout.");

            var refreshToken = await _userRefreshTokenRepository.GetByTokenAsync(refreshTokenValue, cancellationToken);

            if (refreshToken == null || 
                refreshToken.IsRevoked || 
                refreshToken.IsExpired())
                return Result<Unit>.Success(Unit.Value);

            refreshToken.RevokeToken();

            await _userRefreshTokenRepository.UpdateAsync(refreshToken, cancellationToken);

            return Result<Unit>.Success(Unit.Value);
        }

        /// <summary>
        /// Асинхронно получает список строковых ролей пользователя.
        /// </summary>
        /// <param name="userId"> Идентификатор пользователя. </param>
        /// <param name="cancellationToken"> Токен отмены операции. </param>
        /// <returns> Список ролей пользователя. </returns>
        private async Task<List<string>> GetUserRolesAsync(Guid userId, CancellationToken cancellationToken)
        {
            var userRoleConnections = await _userRoleRepository.GetByUserIdAsync(userId, cancellationToken);
            var userRoles = new List<string>();

            if (userRoleConnections != null)
            {
                foreach (var userRoleConn in userRoleConnections)
                {
                    if (userRoleConn.IsActive)
                    {
                        var role = await _roleRepository.GetByIdAsync(userRoleConn.RoleId, cancellationToken);
                        if (role != null)
                            userRoles.Add(role.Type.ToString());
                    }
                }
            }

            return userRoles;
        }
    }
}