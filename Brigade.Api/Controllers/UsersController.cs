using Brigade.Application.User.Command.AuthUser.Validator;
using Brigade.Application.User.Command.RegisterUser;
using Brigade.Application.User.Command.RegisterUser.Validators;
using Brigade.Application.User.Services;
using Brigade.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Brigade.Api.Controllers
{
    /// <summary>
    /// Контроллер API для управления пользователями.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserRegistrationService _userRegistrationService;
        private readonly RegisterUserCommandValidator _validator;
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(UserRegistrationService userRegistrationService,
                               RegisterUserCommandValidator validator,
                               IUnitOfWork unitOfWork)
        {
            _userRegistrationService = userRegistrationService;
            _validator = validator;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Регистрирует нового пользователя.
        /// </summary>
        /// <param name="command"> Объект <see cref="RegisterUserCommand"/>, 
        /// содержащий данные нового пользователя.</param>
        /// <returns> Возвращает 201 Created с идентификатором нового пользователя в случае успеха 
        /// или 400 Bad Request с ошибками в случае неудачи. </returns>
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterUserCommand command)
        {
            var validationResult = await _validator.ValidateAsync(command);
            if (!validationResult.IsValid)
                return BadRequest(new
                {
                    errors = validationResult.Errors
                });

            var result = await _userRegistrationService.RegisterAsync(command);
            if (!result.IsSuccess)
                return BadRequest(new
                {
                    message = result.Errors
                });

            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(Register), new
            {
                id = result.Value
            }, result.Value);
        }
    }
}