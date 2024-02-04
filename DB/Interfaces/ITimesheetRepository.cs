using Entities;

namespace DB.Interfaces
{
    public interface ITimesheetRepository
    {
        /// <summary>
        /// Создаёт запись об отсутствии в бд
        /// </summary>
        public Task CreateRowAsync(TimesheetElement item);

        /// <summary>
        /// Удаляет запись об отсутствии из бд по Id
        /// </summary>
        public Task DeleteRowAsync(int id);

        /// <summary>
        /// Возвращает все записи об отсутствии из бд без TimesheetElement.Desc с подстановкой TimesheetElement.Employee и TimesheetElement.Reason
        /// </summary>
        public Task<List<TimesheetElement>> GetAllRowsAsync();

        /// <summary>
        /// Возвращает информацию по конкретной записи в бд с TimesheetElement.Desc с подстановкой TimesheetElement.EmployeeId и TimesheetElement.ReasonId
        /// </summary>
        public Task<TimesheetElement> GetDetailsRowAsync(int id);

        /// <summary>
        /// Обновляет запись в бд по TimesheetElement.Id
        /// </summary>
        public Task UpdateRowAsync(TimesheetElement item);
    }
}
