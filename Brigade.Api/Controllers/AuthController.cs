using Microsoft.AspNetCore.Mvc;
using Brigade.Application.User.Command.AuthUser;
using Brigade.Application.User.Command.AuthUser.Validator; 
using Brigade.Application.User.Services; 
using Brigade.Domain.Repositories;
using Brigade.Domain.Constants;

namespace Brigade.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class AuthController : ControllerBase
    {
        private readonly UserAuthService _userAuthService;
        private readonly AuthUserCommandValidator _validator; 
        private readonly IUnitOfWork _unitOfWork;

        public AuthController(UserAuthService userAuthService,
                              AuthUserCommandValidator validator,
                              IUnitOfWork unitOfWork) 
        {
            _userAuthService = userAuthService;
            _validator = validator;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Аутентифицирует пользователя по email и паролю.
        /// </summary>
        /// <param name="command"> Объект <see cref="AuthUserCommand"/>. </param>
        /// <returns> Возвращает 200 OK с <see cref="AuthResult"/> в случае успеха
        /// или 400 Bad Request с ошибкой в случае неудачи. </returns>
        [HttpPost("login")] 
        public async Task<ActionResult> Login([FromBody] AuthUserCommand command)
        {
            var validationResult = await _validator.ValidateAsync(command);
            if (!validationResult.IsValid)
                return BadRequest(new { 
                    errors = validationResult.Errors 
                });

            var result = await _userAuthService.AuthAsync(command);

            if (!result.IsSuccess)
                return BadRequest(new { 
                    message = result.Errors 
                });

            await _unitOfWork.SaveChangesAsync();

            var refreshTokenValue = result.Value!.RefreshToken; 
            var refreshTokenExpiry = result.Value.AccessTokenExpiry;

            Response.Cookies.Append(
                "refreshToken", 
                refreshTokenValue, 
                new CookieOptions
                {
                    HttpOnly = true, 
                    Secure = true, 
                    SameSite = SameSiteMode.Strict,
                    // на будущее решить, пока на начале работы нет идей
                    Path = "/api/auth", 
                    MaxAge = TimeSpan.FromDays(Consts.REFRESH_TOKEN_EXPIRES),
                    Expires = refreshTokenExpiry 
                }
            );

            var responseResult = new 
            {
                result.Value.UserId,
                result.Value.AccessToken,
                result.Value.AccessTokenExpiry
            };

            return Ok(responseResult);
        }

        /// <summary>
        /// Обновляет токены доступа на основе refresh токена, полученного из куки.
        /// </summary>
        /// <returns>Возвращает 200 OK с <see cref="AuthResult"/> в случае успеха или 400 Bad Request с ошибкой в случае неудачи.</returns>
        [HttpPut("refresh")]
        public async Task<ActionResult> Refresh()
        {
            var refreshTokenFromCookie = Request.Cookies["refreshToken"]; 

            if (string.IsNullOrEmpty(refreshTokenFromCookie))
                return BadRequest(new { 
                    message = "Refresh токен не найден в куки." 
                });

            var result = await _userAuthService.RefreshTokenAsync(refreshTokenFromCookie);

            if (!result.IsSuccess)
            {
                Response.Cookies.Delete("refreshToken");
                return BadRequest(new { 
                    message = result.Errors 
                });
            }

            var newRefreshTokenValue = result.Value!.RefreshToken;
            var newRefreshTokenExpiry = result.Value.AccessTokenExpiry;

            Response.Cookies.Append(
                "refreshToken",
                newRefreshTokenValue,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true, 
                    SameSite = SameSiteMode.Strict,
                    Path = "/api/auth", 
                    Expires = newRefreshTokenExpiry
                }
            );

            var responseResult = new
            {
                result.Value.UserId,
                result.Value.AccessToken,
                result.Value.AccessTokenExpiry
            };

            await _unitOfWork.SaveChangesAsync();

            return Ok(responseResult);
        }

        /// <summary>
        /// Отзывает refresh токен, полученный из куки, и выполняет выход пользователя из системы.
        /// </summary>
        /// <returns> Возвращает 200 OK в случае успеха или 400 Bad Request с ошибкой в случае неудачи. </returns>
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            var refreshTokenFromCookie = Request.Cookies["refreshToken"]; 

            if (string.IsNullOrEmpty(refreshTokenFromCookie))
            {
                Response.Cookies.Delete("refreshToken");
                return Ok(); 
            }

            var result = await _userAuthService.LogoutAsync(refreshTokenFromCookie);

            if (!result.IsSuccess)
                return BadRequest(new { 
                    message = result.Errors 
                });

            Response.Cookies.Delete("refreshToken");

            await _unitOfWork.SaveChangesAsync();

            return Ok(); 
        }
    }
}