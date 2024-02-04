using BLL.Interfaces;
using DB.Interfaces;
using Entities;

namespace BLL.Services
{
    public class ReasonService : IReasonService
    {
        private IReasonRepository _repository;
        public ReasonService(IReasonRepository Repository)
        {
            _repository = Repository;
        }

        public async Task<List<Reason>> GetAllItemsAsync()
        {

            return await _repository.GetAllReasonsAsync();
        }
    }
}
