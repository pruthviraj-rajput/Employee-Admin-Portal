using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
    //xxxx/api/Employees
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var allEmployee = dbContext.Employees.ToList();
            return Ok(allEmployee);
        }




        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployees(Guid id)
        {
            var employee = dbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        //[HttpGet]
        //public IActionResult GetEmployeesByName(string name)
        //{
        //    var employee = dbContext.Employees.Find(name);
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(employee);
        //}



        [HttpPost]
        public IActionResult AddEmployees(AddEmployeeDto addEmployeeDto)
        {
            var employeeEntity = new Employee()
            {
                Email = addEmployeeDto.Email,
                Name = addEmployeeDto.Name,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary,
            };


            dbContext.Employees.Add(employeeEntity);
            dbContext.SaveChanges();
            return Ok(employeeEntity);
        }


        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {

            var employee = dbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            employee.Name = updateEmployeeDto.Name;
            employee.Phone = updateEmployeeDto.Phone;
            employee.Salary = updateEmployeeDto.Salary;
            employee.Email = updateEmployeeDto.Email;

            dbContext.SaveChanges();
            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public IActionResult DeleteEmployees(Guid id)
        {
            var employee = dbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            dbContext.Employees.Remove(employee);
            dbContext.SaveChanges();
            return Ok(employee.Name);

        }
    }
}

