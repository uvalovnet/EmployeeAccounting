using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAccounting.Controllers
{
    [ApiController]
    [Route("Reasons")]
    public class ReasonController : Controller
    {
        private IReasonService _service;
        public ReasonController(IReasonService service)
        {
            _service = service;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                return Json(await _service.GetAllItemsAsync());
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
