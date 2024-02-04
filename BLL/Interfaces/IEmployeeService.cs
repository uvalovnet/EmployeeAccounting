using Entities;

namespace BLL.Interfaces
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Создание сотрудника
        /// </summary>
        public Task CreateItemAsync(Employee item);

        /// <summary>
        /// Определяет какой метод удаляения вызвать
        /// </summary>
        public Task DeleteItemAsync(int id, int rowsQty);

        /// <summary>
        /// Возвращает всех сотрудников
        /// </summary>
        public Task<List<Employee>> GetAllItemsAsync();

        /// <summary>
        /// Возвращает все данные об одном сотруднике
        /// </summary>
        public Task<Employee> GetDetailsItemAsync(int id);

        /// <summary>
        /// Обновление данных о сотруднике
        /// </summary>
        public Task UpdateItemAsync(Employee item);
    }
}
