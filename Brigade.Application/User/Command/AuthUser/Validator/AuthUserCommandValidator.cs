using FluentValidation;

namespace Brigade.Application.User.Command.AuthUser.Validator
{
    /// <summary>
    /// Валидация <see cref="AuthUserCommandValidator"/>.
    /// </summary>
    public class AuthUserCommandValidator : AbstractValidator<AuthUserCommand>
    {
        /// <summary>
        /// Валидатор для <see cref="AuthUserCommandValidator"/>.
        /// Проверяет корректность данных, передаваемых при авторизации пользователя.
        /// </summary>
        public AuthUserCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty()
                                 .WithMessage("Почта должна быть заполнена")
                                 .EmailAddress()
                                 .WithMessage("Почта имеет некорректный формат");

            RuleFor(x => x.Password).NotEmpty()
                                    .WithMessage("Пароль обязателен.");
        }
    }
}