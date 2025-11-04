namespace Brigade.Domain.Repositories
{
    /// <summary>
    /// Представляет шаблон единицы работы (Unit of Work) для управления транзакциями и сохранением изменений в репозиториях.
    /// Обеспечивает атомарность операций, связанных с несколькими репозиториями.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Асинхронно сохраняет все изменения, внесённые в контекст данных, в базу данных.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Результатом является количество строк, затронутых при сохранении.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}