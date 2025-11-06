using Brigade.Domain.Constants;
using Brigade.Domain.Enums;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Brigade.Application.User.Command.RegisterUser.Validators
{
    /// <summary>
    /// Валидация <see cref="RegisterUserCommand"/>.
    /// </summary>
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        /// <summary>
        /// Регулярное выражение для валидации мобильного телефона.
        /// </summary>
        private static readonly Regex PhoneRegex = new(
            @"^[\+]?[0-9]{1,4}?[-\s\.]?[0-9]{3,4}[-\s\.]?[0-9]{3,4}[-\s\.]?[0-9]{3,4}$",
            RegexOptions.Compiled
        );

        /// <summary>
        /// Валидатор для <see cref="RegisterUserCommand"/>.
        /// Проверяет корректность данных, передаваемых при регистрации пользователя.
        /// </summary>
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty()
                                 .WithMessage("Почта должна быть заполнена")
                                 .EmailAddress()
                                 .WithMessage("Почта имеет некорректный формат");

            RuleFor(x => x.Password).NotEmpty()
                                    .WithMessage("Пароль обязателен.")
                                    .MinimumLength(Consts.MIN_PASSWORD_LENGTH)
                                    .WithMessage("Пароль должен содержать не менее 8 символов.")
                                    .Matches(@"[A-Z]")
                                    .WithMessage("Пароль должен содержать хотя бы одну заглавную букву.")
                                    .Matches(@"[a-z]")
                                    .WithMessage("Пароль должен содержать хотя бы одну строчную букву.")
                                    .Matches(@"[0-9]")
                                    .WithMessage("Пароль должен содержать хотя бы одну цифру.")
                                    .Matches(@"[\W_]")
                                    .WithMessage("Пароль должен содержать хотя бы один специальный символ.");

            RuleFor(x => x.FullName).NotEmpty()
                                    .WithMessage("Имя обязательно.")
                                    .MaximumLength(Consts.MAX_NAME_LENGTH)
                                    .WithMessage("Имя не должно превышать 255 символов.");

            RuleFor(x => x.Phone).NotEmpty()
                                 .WithMessage("Телефон обязателен.")
                                 .Matches(PhoneRegex)
                                 .WithMessage("Некорректный формат телефона.");

            RuleFor(x => x.PreferencesContact).Must((command, contact) 
                                                => string.IsNullOrEmpty(contact) || 
                                                Enum.TryParse<PreferencesContactMethod>(contact, true, out _))
                                              .WithMessage("Некорректный метод связи.");

            RuleFor(x => x.RegionId).NotEmpty()
                                    .WithMessage("Регион обязателен.")
                                    .Must(BeAValidGuid)
                                    .WithMessage("Регион должен быть валидным GUID.");

            RuleFor(x => x.UserRole).NotEmpty()
                                    .WithMessage("Необходимо выбрать роль пользователя")
                                    .Must(type => Enum.TryParse<RoleType>(type, true, out _))
                                    .WithMessage("Некорректный тип пользователя.");

            When(x => x.UserRole.Equals("Company", StringComparison.OrdinalIgnoreCase), () =>
            {
                RuleFor(x => x.CompanyLegalName).NotEmpty()
                                                .WithMessage("Юридическое наименование компании обязательно для типа 'Company'.")
                                                .MaximumLength(Consts.MAX_NAME_LENGTH)
                                                .WithMessage("Юридическое наименование не должно превышать 255 символов.");

                RuleFor(x => x.CompanyINN).NotEmpty()
                                          .WithMessage("ИНН компании обязателен для типа 'Company'.")
                                          .Length(Consts.INN_LENGTH)
                                          .WithMessage("ИНН должен содержать 11 цифр.");
                                          
            });
        }

        /// <summary>
        /// Проверяет, является ли строка валидным GUID.
        /// </summary>
        /// <param name="guid"> Значение для валидации. </param>
        /// <returns> <see langword="true"/> - если Guid валиден, иначе - <see langword="false" />. </returns>
        private bool BeAValidGuid(string guid)
            => Guid.TryParse(guid, out _);
    }
}