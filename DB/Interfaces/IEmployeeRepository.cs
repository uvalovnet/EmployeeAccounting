using Entities;

namespace DB.Interfaces
{
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Создаёт из сущности запись в бд 
        /// </summary>
        public Task CreateEmployeeAsync(Employee item);

        /// <summary>
        /// Удаляет сотрудника из бд по id если в таблице timesheet нет о нём записей
        /// </summary>
        public Task DeleteEmployeeAsync(int id);

        /// <summary>
        /// Удаляет сотрудника и записи о нём из бд по id если в таблице timesheet есть о нём записи
        /// </summary>
        public Task DeleteEmployeeWithHisTimesheetAsync(int id);

        /// <summary>
        /// Возвращает список сотрудников с их id в бд
        /// </summary>
        public Task<List<Employee>> GetAllEmployeesAsync();

        /// <summary>
        /// Возвращает всю информацию о сотруднике из бд
        /// </summary>
        public Task<Employee> GetDetailsEmployeeAsync(int id);

        /// <summary>
        /// Обновляет информацию о сотруднике по Employee.Id
        /// </summary>
        public Task UpdateEmployeeAsync(Employee item);
    }
}
