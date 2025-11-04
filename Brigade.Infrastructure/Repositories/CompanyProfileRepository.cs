using Brigade.Domain.Entities;
using Brigade.Domain.Repositories;
using Brigade.Infrastructure.Data;

namespace Brigade.Infrastructure.Repositories
{
    /// <summary>
    /// Реализация репозитория для управления профилями компаний в базе данных.
    /// </summary>
    public class CompanyProfileRepository : ICompanyProfileRepository
    {
        private readonly BrigadeDbContext _context;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CompanyProfileRepository"/> 
        /// с указанным контекстом базы данных.
        /// </summary>
        /// <param name="context"> Контекст базы данных Entity Framework. </param>
        /// <exception cref="ArgumentNullException">
        /// Выбрасывается, если <paramref name="context"/> равен <see langword="null"/>.
        /// </exception>
        public CompanyProfileRepository(BrigadeDbContext context)
            => _context = context ?? throw new ArgumentNullException(nameof(context));

        /// <summary>
        /// Асинхронно добавляет профиль компании в контекст базы данных.
        /// Изменения будут сохранены при вызове <see cref="BrigadeDbContext.SaveChangesAsync"/>.
        /// </summary>
        /// <param name="profile"> Профиль компании для добавления. </param>
        /// <param name="cancellationToken"> Токен отмены операции. </param>
        /// <returns> Задача, представляющая асинхронную операцию. </returns>
        /// <exception cref="ArgumentNullException">
        /// Выбрасывается, если <paramref name="profile"/> равен <see langword="null"/>.
        /// </exception>
        public async Task AddAsync(CompanyProfile profile, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(profile);

            await _context.CompanyProfiles.AddAsync(profile, cancellationToken);
        }
    }
}