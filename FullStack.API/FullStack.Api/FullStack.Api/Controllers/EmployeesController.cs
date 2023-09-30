using FullStack.Api.Data;
using FullStack.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullStack.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly FullstackDbContext _fullstackDbContext;

        public EmployeesController(FullstackDbContext fullstackDbContext) 
        {
            _fullstackDbContext = fullstackDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _fullstackDbContext.Employees.ToListAsync();

            return Ok(employees);
        }


        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employeeRequest)
        {

            employeeRequest.Id = Guid.NewGuid();
            await _fullstackDbContext.Employees.AddAsync(employeeRequest);
            await _fullstackDbContext.SaveChangesAsync();

            return Ok(employeeRequest);
        }


        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await _fullstackDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if(employee == null) { return NotFound(); }

            return Ok(employee);
        }


        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id , Employee updateEmployeeRequest)
        {
            var employee = await _fullstackDbContext.Employees.FindAsync(id);
            if (employee == null) { return NotFound(); }

            employee.Name = updateEmployeeRequest.Name;
            employee.Email = updateEmployeeRequest.Email;
            employee.Phone = updateEmployeeRequest.Phone;
            employee.Salary = updateEmployeeRequest.Salary;
            employee.Department = updateEmployeeRequest.Department;

            await _fullstackDbContext.SaveChangesAsync();
            
            return Ok(employee);

        }


        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var employee = await _fullstackDbContext.Employees.FindAsync(id);
            if (employee == null) { return NotFound(); }

            _fullstackDbContext.Remove(employee);
            await _fullstackDbContext.SaveChangesAsync();

            return Ok(employee);
        }

    }
}
