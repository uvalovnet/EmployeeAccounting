using BLL.Interfaces;
using DB.Interfaces;
using Entities;

namespace BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository _repository;
        public EmployeeService(IEmployeeRepository Repository)
        {
            _repository = Repository;
        }

        public async Task CreateItemAsync(Employee item)
        {
            await _repository.CreateEmployeeAsync(item);
        }

        public async Task DeleteItemAsync(int id, int rowsQty)
        {
            if (rowsQty == 0) { await _repository.DeleteEmployeeAsync(id); }
            else { await _repository.DeleteEmployeeWithHisTimesheetAsync(id); }
        }

        public async Task<List<Employee>> GetAllItemsAsync()
        {
            return await _repository.GetAllEmployeesAsync();
        }

        public async Task<Employee> GetDetailsItemAsync(int id)
        {
            return await _repository.GetDetailsEmployeeAsync(id);
        }

        public async Task UpdateItemAsync(Employee item)
        {
            await _repository.UpdateEmployeeAsync(item);
        }
    }
}
