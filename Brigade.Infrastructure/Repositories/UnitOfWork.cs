using Brigade.Domain.Repositories;
using Brigade.Infrastructure.Data;
namespace Brigade.Infrastructure.Repositories
{
    /// <summary>
    /// Реализация Unit of Work на основе BrigadeDbContext.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BrigadeDbContext _context;

        public UnitOfWork(BrigadeDbContext context)
            => _context = context;

        /// <summary>
        /// Асинхронно сохраняет все изменения, сделанные в контексте базы данных.
        /// </summary>
        /// <param name="cancellationToken"> Токен отмены для прерывания операции. </param>
        /// <returns> Количество записей, затронутых в базе данных. </returns>
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await _context.SaveChangesAsync(cancellationToken);

        /// <summary>
        /// Освобождает ресурсы контекста базы данных
        /// </summary>
        public void Dispose()
            => _context?.Dispose();
    }
}