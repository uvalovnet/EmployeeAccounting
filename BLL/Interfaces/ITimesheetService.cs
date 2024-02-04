using Entities;

namespace BLL.Interfaces
{
    public interface ITimesheetService
    {
        /// <summary>
        /// Создание записи об отсутствии
        /// </summary>
        public Task CreateItemAsync(TimesheetElement item);

        /// <summary>
        /// Удаление записи об отсутствии по Id
        /// </summary>
        public Task DeleteItemAsync(int id);

        /// <summary>
        /// Получение всех записей об отсутствии без TimesheetElement.Desc, TimesheetElement.EmployeeId и TimesheetElement.ReasonId с подстановкой TimesheetElement.Employee и TimesheetElement.Reason
        /// </summary>
        public Task<List<TimesheetElement>> GetAllItemsAsync();

        /// <summary>
        /// Получение деталей одной записи об отсутствии по Id с TimesheetElement.Desc, TimesheetElement.EmployeeId и TimesheetElement.ReasonId без TimesheetElement.Employee и TimesheetElement.Reason
        /// </summary>
        public Task<TimesheetElement> GetDetailItemAsync(int id);

        /// <summary>
        /// Обновление записи об отсутствии по TimesheetElement.Id
        /// </summary>
        public Task UpdateItemAsync(TimesheetElement item);
    }
}
