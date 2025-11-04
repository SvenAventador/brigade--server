using Brigade.Domain.Entities;
using Brigade.Domain.Repositories;
using Brigade.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Brigade.Infrastructure.Repositories
{
    /// <summary>
    /// Реализация репозитория для доступа к данным пользователей в базе данных.
    /// Загружает связанные сущности (регион, роли) при получении пользователя.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly BrigadeDbContext _context;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="UserRepository"/> с указанным контекстом базы данных.
        /// </summary>
        /// <param name="context"> Контекст базы данных Entity Framework. </param>
        /// <exception cref="ArgumentNullException">
        /// Выбрасывается, если <paramref name="context"/> равен <see langword="null"/>.
        /// </exception>
        public UserRepository(BrigadeDbContext context)
            => _context = context ?? throw new ArgumentNullException(nameof(context));

        /// <summary>
        /// Асинхронно добавляет нового пользователя в контекст базы данных.
        /// Изменения будут сохранены при вызове <see cref="BrigadeDbContext.SaveChangesAsync"/>.
        /// </summary>
        /// <param name="user"> Пользователь для добавления. </param>
        /// <param name="cancellationToken"> Токен отмены операции. </param>
        /// <returns> Задача, представляющая асинхронную операцию. </returns>
        /// <exception cref="ArgumentNullException">
        /// Выбрасывается, если <paramref name="user"/> равен <see langword="null"/>.
        /// </exception>
        public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(user);

            await _context.Users.AddAsync(user, cancellationToken);
        }

        /// <summary>
        /// Асинхронно получает пользователя по адресу электронной почты, включая связанные данные:
        /// регион (<see cref="User.Region"/>) и роли через коллекцию <see cref="User.UserRoles"/>.
        /// </summary>
        /// <param name="email"> Адрес электронной почты пользователя. </param>
        /// <param name="cancellationToken"> Токен отмены операции. </param>
        /// <returns>
        /// Экземпляр <see cref="User"/>, если пользователь с указанным email найден;
        /// в противном случае — <see langword="null"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Выбрасывается, если <paramref name="email"/> равен <see langword="null"/> или пуст.
        /// </exception>
        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email не может быть пустым.", nameof(email));

            return await _context.Users
                                 .Include(x => x.Region)
                                 .Include(x => x.UserRoles)
                                     .ThenInclude(ur => ur.Role)
                                 .FirstOrDefaultAsync(x => x.Email.Value == email, cancellationToken);
        }

        /// <summary>
        /// Асинхронно получает пользователя по уникальному идентификатору, включая связанные данные:
        /// регион (<see cref="User.Region"/>) и роли через коллекцию <see cref="User.UserRoles"/>.
        /// </summary>
        /// <param name="id"> Уникальный идентификатор пользователя. </param>
        /// <param name="cancellationToken"> Токен отмены операции. </param>
        /// <returns>
        /// Экземпляр <see cref="User"/>, если пользователь с указанным ID найден;
        /// в противном случае — <see langword="null"/>.
        /// </returns>
        public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _context.Users
                             .Include(x => x.Region)
                             .Include(x => x.UserRoles)
                                 .ThenInclude(ur => ur.Role)
                             .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}