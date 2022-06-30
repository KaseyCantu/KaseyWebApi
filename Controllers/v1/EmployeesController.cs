using KaseyWebApi.DataModel;
using KaseyWebApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KaseyWebApi.Controllers.v1;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class EmployeesController : Controller
{
    private readonly IEmployees _employeeContext;

    public EmployeesController(IEmployees employee)
    {
        _employeeContext = employee;
    }

    // GET: api/v1/employee>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> Get()
    {
        return await Task.FromResult(_employeeContext.GetEmployees());
    }

    // GET api/v1/employee/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetEmployeeById(int id)
    {
        var employees = await Task.FromResult(_employeeContext.GetEmployeeDetails(id));
        if (employees == null)
        {
            return NotFound();
        }

        return employees;
    }

    // POST api/v1/employee
    [HttpPost]
    public async Task<ActionResult<Employee>> Post(Employee employee)
    {
        _employeeContext.AddEmployee(employee);
        return await Task.FromResult(employee);
    }

    // PUT api/v1/employee/5
    [HttpPut("{id}")]
    public async Task<ActionResult<Employee>> Put([FromRoute] int id, [FromBody] Employee employee)
    {
        if (!EmployeeExists(id))
        {
            return NotFound(new { message = $"User with EmployeeId: {id.ToString()} does not exist" });
        }

        if (id != employee.EmployeeId)
        {
            return BadRequest();
        }

        try
        {
            _employeeContext.UpdateEmployee(employee);
            return await Task.FromResult(Ok(new { message = $"User with EmployeeId: {id} was updated successfully" }));
        }
        catch (DbUpdateConcurrencyException ex)
        {
            if (!EmployeeExists(id))
            {
                return NotFound();
            }

            return BadRequest(ex.Message);
        }
    }

    // DELETE api/v1/employee/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Employee>> Delete([FromRoute] int id)
    {
        var employee = _employeeContext.DeleteEmployee(id);
        return await Task.FromResult(employee);
    }

    private bool EmployeeExists(int id)
    {
        return _employeeContext.CheckEmployee(id);
    }
}