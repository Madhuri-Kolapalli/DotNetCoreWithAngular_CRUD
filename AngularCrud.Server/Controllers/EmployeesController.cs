using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AngularCrud.Server.Data;
using AngularCrud.Server.Models;

namespace AngularCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly AngularCrudServerContext _context;

        public EmployeesController(AngularCrudServerContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("SearchEmployees")]
        public async Task<IEnumerable<Employee>> SearchEmployees([FromQuery] string query)
        {
            return await _context.Employee
                 .Where(e => (e.Id.ToString().Contains(query)|| e.Name.ToString().Contains(query)) && !e.Isdelete && e.IsActive)
                 .ToListAsync();
        }
        // GET: api/Employees
        [HttpGet]
        [Route("GetAllEmployee")]
        public async Task<IEnumerable<Employee>> GetEmployee()
        {
            return await _context.Employee
                 .Where(e => !e.Isdelete && e.IsActive)
                 .ToListAsync();
        }
        [HttpGet]
        [Route("TopEmployees")]
        public async Task<IEnumerable<Employee>> TopEmployees()
        {
            return await _context.Employee
                 .Where(e => !e.Isdelete && e.IsActive)
                 .OrderBy(e => e.Id)
                 .Take(5)
                 .ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.Employee.FindAsync(id);

            if (employee == null || employee.Isdelete )
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("PutEmployee")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            employee.Isdelete = false;
            employee.IsActive = true;// Ensure new employees are not marked as deleted
            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        // DELETE: api/Employees/5
        [HttpPut]
        [Route("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var employee = await _context.Employee.FindAsync(id);
                if (employee == null)
                {
                    return NotFound();
                }
                employee.Isdelete = true;
                employee.IsActive = false;
                _context.Employee.Update(employee);
                await _context.SaveChangesAsync();

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }
    }

    internal class HttpdeleteAttribute : Attribute
    {
    }
}
