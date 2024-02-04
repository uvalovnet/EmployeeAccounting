using Entities;

namespace BLL.Interfaces
{
    public interface IReasonService
    {
        /// <summary>
        /// Возвращает все записи
        /// </summary>
        public Task<List<Reason>> GetAllItemsAsync();
    }
}
