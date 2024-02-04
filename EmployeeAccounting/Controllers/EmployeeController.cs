using BLL.Interfaces;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAccounting.Controllers
{
    [ApiController]
    [Route("Employees")]
    public class EmployeeController : Controller
    {
        private IEmployeeService _service;
        public EmployeeController(IEmployeeService service)
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
        public async Task<IActionResult> GetDetailsAsync(int id)
        {
            try
            {
                return Json(await _service.GetDetailsItemAsync(id));
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync(Employee item)
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
        public async Task<IActionResult> UpdateAsync(Employee item)
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

        [HttpDelete("Delete/{id};{rowsQty}")]
        public async Task<IActionResult> DeleteAsync(int id, int rowsQty)
        {
            try
            {
                await _service.DeleteItemAsync(id, rowsQty);
                return StatusCode(200);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
