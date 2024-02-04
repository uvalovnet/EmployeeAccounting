using BLL.Interfaces;
using DB.Interfaces;
using Entities;

namespace BLL.Services
{
    public class TimesheetService : ITimesheetService
    {
        private ITimesheetRepository _repository;
        public TimesheetService(ITimesheetRepository Repository)
        {
            _repository = Repository;
        }

        public async Task CreateItemAsync(TimesheetElement item)
        {
            await _repository.CreateRowAsync(item);
        }

        public async Task DeleteItemAsync(int id)
        {
            await _repository.DeleteRowAsync(id);
        }

        public async Task<List<TimesheetElement>> GetAllItemsAsync()
        {
            return await _repository.GetAllRowsAsync();
        }

        public async Task<TimesheetElement> GetDetailItemAsync(int id)
        {

            return await _repository.GetDetailsRowAsync(id);
        }

        public async Task UpdateItemAsync(TimesheetElement item)
        {
            await _repository.UpdateRowAsync(item);
        }
    }
}
