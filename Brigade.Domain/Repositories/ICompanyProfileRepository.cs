using Brigade.Domain.Entities;

namespace Brigade.Domain.Repositories
{
    /// <summary>
    /// Предоставляет методы для добавления профилей компаний в хранилище.
    /// </summary>
    public interface ICompanyProfileRepository
    {
        /// <summary>
        /// Асинхронно добавляет новый профиль компании в хранилище.
        /// </summary>
        /// <param name="profile"> Профиль компании для добавления. </param>
        /// <param name="cancellationToken"> Токен отмены операции. </param>
        /// <returns> Задача, представляющая асинхронную операцию. </returns>
        Task AddAsync(CompanyProfile profile, CancellationToken cancellationToken = default);
    }
}