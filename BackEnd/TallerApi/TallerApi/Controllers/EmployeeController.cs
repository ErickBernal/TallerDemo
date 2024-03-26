/*
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TallerApi.Data.Models;
using TallerApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TallerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly EmployeeService _service;

        public EmployeeController(EmployeeService es)
        {
            _service = es;
        }


        // GET: api/<EmployeeController>
        [HttpGet]
        public async Task<IEnumerable<Employee>> Get()
        {
            return await _service.GetAll();
        }


        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetById(int id)
        {
            var employe = await _service.GetById(id);
            if (employe == null)
            {
                return EmployeeNotFound(id);
            }
            else
            {
                return employe;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            var newEmployee = await _service.Create(employee);
            return CreatedAtAction(nameof(GetById), new { IdEmployee = newEmployee.IdEmployee }, newEmployee);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Employee employee)
        {
            if (id != employee.IdEmployee)
                return BadRequest(new { message = $"El Id={id} de la URL no coincide con el id({employee.IdEmployee}) del cuerpo  de la solicitud" });

            var employeeToUpdate = await _service.GetById(id);
            if (employeeToUpdate != null)
            {
                await _service.Update(id, employee);
                return NoContent();
            }
            else
            {
                return EmployeeNotFound(id);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employeeToDelete = await _service.GetById(id);
            if (employeeToDelete != null)
            {
                await _service.Delete(employeeToDelete);
                return NoContent();
            }
            else
            {
                return EmployeeNotFound(id);
            }

        }
        private NotFoundObjectResult EmployeeNotFound(int id)
        {
            return NotFound(new { message = $"El cliente con Id={id} no existe" });
        }
    }
}
*/