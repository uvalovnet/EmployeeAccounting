using BLL.Interfaces;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAccounting.Controllers
{
    [ApiController]
    [Route("Timesheet")]
    public class TimesheetController : Controller
    {
        private ITimesheetService _service;
        public TimesheetController(ITimesheetService service)
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetailAsync(int id)
        {
            try
            {
                return Json(await _service.GetDetailItemAsync(id));
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync(TimesheetElement item)
        {
            try
            {
                await _service.CreateItemAsync(item);
                return StatusCode(200);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAsync(TimesheetElement item)
        {
            try
            {
                await _service.UpdateItemAsync(item);
                return StatusCode(200);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _service.DeleteItemAsync(id);
                return StatusCode(200);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
