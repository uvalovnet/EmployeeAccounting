using Entities;

namespace DB.Interfaces
{
    public interface IReasonRepository
    {
        /// <summary>
        /// Возвращает все записи из таблицы
        /// </summary>
        public Task<List<Reason>> GetAllReasonsAsync();
    }
}
