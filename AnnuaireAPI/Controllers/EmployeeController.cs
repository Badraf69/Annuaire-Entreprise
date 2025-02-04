using System.Text.RegularExpressions;
using AnnuaireModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnnuaireAPI.Controllers;
[ApiController]
//[Authorize]

public class EmployeeController : ControllerBase
{

    private readonly ILogger<EmployeeController> _logger;
    
    private readonly AppDbContext appContext;
    
    public EmployeeController(AppDbContext context, ILogger<EmployeeController> logger)
    {
        appContext = context;
        _logger = logger;
    }
    // Route API pour récupérer tous les employés
    [HttpGet("GetAll[controller]s",Name = "GetAllEmployees")]
    public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
    {
        try
        {
            var employees = await appContext.Employees
                .Include(e=>e.Service)
                .Include(e=>e.Site)
                .ToListAsync();
            if (employees.Count == 0)
            {
                return NotFound(new { message = "No employees found." });
            }
            return Ok(employees);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "erreur lors du getEmployees: {Message}", ex.Message);
            return StatusCode(500,$"Erreur interne du serveur: {ex.Message}");
        }
    }
    
    //Route APi pour récupérer 1 employé via son id
    [HttpGet("Get[controller]ById/{id}", Name = "GetEmployeeById")]
    public async Task<IActionResult> GetEmployeeById(int id)
    {
        try
        {
            var employee = await appContext.Employees
                .Include(e=>e.Service)
                .Include(e =>e.Site)
                .FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null){
                return NotFound(new { message = "Employee not found" });
            }
            return Ok(employee);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "erreur lors du getEmployeeById: {Message}", ex.Message);
            return StatusCode(500,$"Erreur interne du serveur: {ex.Message}");
        }
        
    }
    
    //Route APi pour ajouter un employe    
    [HttpPost("Add[controller]", Name = "AddEmployee")]
    public  async Task<IActionResult> AddEmployee(Employee employee)
    {
        try
        {
            if (employee == null)
            {
                return BadRequest(new { message = "Invalid data." });
            }
            
            await appContext.Employees.AddAsync(employee);
            await appContext.SaveChangesAsync();
            return CreatedAtRoute("GetEmployeeById", new { id = employee.Id }, employee);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "erreur lors de l'ajout d'un employe: {Message}", ex.Message);
            return StatusCode(500,$"Erreur interne du serveur: {ex.Message}");
        }
        
    }
    
    //Route API pour supprimer un employe via son id
    [HttpDelete("delete[controller]/{id}", Name = "DeleteEmployee")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        try
        {
            var employee = await appContext.Employees
                .FindAsync(id);
            if (employee == null)
            {
                return NotFound(new { message = "Employee not found" });
            }
            appContext.Employees.Remove(employee);
            await appContext.SaveChangesAsync();
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "erreur lors du DeleteEmployee: {Message}", ex.Message);
            return StatusCode(500,$"Erreur interne du serveur: {ex.Message}");
        }
        
    }
    
    //Route API pour modifier un employe via son id
    [HttpPut("Update[controller]/{id}", Name = "UpdateEmployee")]
    public async Task<IActionResult> UpdateEmployee(int id, Employee updatedEmployee)
    {
        try
        {
            var exisitingEmployee = await appContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (exisitingEmployee == null)
            {
                return NotFound(new { message = "Employee not found" });
            }
    
            exisitingEmployee.FirstName = updatedEmployee.FirstName;
            exisitingEmployee.LastName = updatedEmployee.LastName;
            exisitingEmployee.Email = updatedEmployee.Email;
            exisitingEmployee.Phone = updatedEmployee.Phone;
            exisitingEmployee.CellPhone = updatedEmployee.CellPhone;
            if (updatedEmployee.ServiceId.HasValue)
            {
                exisitingEmployee.ServiceId = updatedEmployee.ServiceId;
            }
    
            if (updatedEmployee.SiteId.HasValue)
            {
                exisitingEmployee.SiteId = updatedEmployee.SiteId;
            }
    
            appContext.Entry(exisitingEmployee).State = EntityState.Modified;
           await appContext.SaveChangesAsync();
            return Ok(exisitingEmployee);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "erreur lors du UpdateEmployeeById: {Message}", ex.Message);
            return StatusCode(500,$"Erreur interne du serveur: {ex.Message}");
        }
    }
}