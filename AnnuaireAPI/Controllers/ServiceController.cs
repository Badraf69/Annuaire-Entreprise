using AnnuaireModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnnuaireAPI.Controllers;
[ApiController]
//[Authorize]
public class ServiceController : ControllerBase 
{
    private readonly ILogger<ServiceController> _logger;
    private readonly AppDbContext appContext;

    public ServiceController(AppDbContext context, ILogger<ServiceController> logger)
    {
        appContext = context;
        _logger = logger;
    }
    //Route API pour récupérer tous les services
    [HttpGet("GetAll[controller]s", Name = "GetAllServices")]
    public ActionResult<IEnumerable<Service>> GetAllServices()
    {
        try
        {
            var services = appContext.Services
                .ToList();
            if (services == null)
            {
                return NotFound(new {message = "There are no services available"});
            }
            return Ok(services);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "erreur lors du getAllService: {Message}", ex.Message);
            return StatusCode(500,$"Erreur interne du serveur: {ex.Message}");
        }
    }
    
    //Route API pour ajouter un service
    [HttpPost("Add[controller]", Name = "AddService")]
    public IActionResult AddService(Service service)
    {
        try
        {
            if (service == null)
            {
                return BadRequest(new { message = "Invalid data." });
            } 
            appContext.Services.Add(service);
            appContext.SaveChanges();
            return CreatedAtRoute("GetServiceById", new { id = service.Id }, service);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "erreur lors du addService: {Message}", ex.Message);
            return StatusCode(500,$"Erreur interne du serveur: {ex.Message}");
        }
    }
    
    //Route API pour supprimer un service via son id
    [HttpDelete("Delete[controller]/{id}", Name = "DeleteService")]
    public IActionResult DeleteService(int id)
    {
        try
        {
            var service = appContext.Services
                .FirstOrDefault(s=>s.Id == id);
            if (service == null)
            {
                return NotFound(new { message = "Service not found." });
            }
            appContext.Services.Remove(service);
            appContext.SaveChanges();
            return NoContent();
        }
        catch (Exception ex)
        {
            if (ex.InnerException.Message == "SQLite Error 19: 'FOREIGN KEY constraint failed'.")
            {
                return StatusCode(501,new {message = ex.InnerException.Message});
            }
            _logger.LogError(ex, "erreur lors du DeleteService: {Message}", ex.Message);
            return StatusCode(500,$"Erreur interne du serveur: {ex.Message}");
        }
    }
    
    //Route API pour modifier un service via son id
    [HttpPut("Update[controller]/{id}", Name = "UpdateService")]
    public IActionResult UpdateService(int id, Service updateService)
    {
        try
        {
            var existingService = appContext.Services.FirstOrDefault(s => s.Id == id);
            if (existingService == null)
            {
                return NotFound(new { message = "Service not found." });
            }
            existingService.ServiceName = updateService.ServiceName;
            appContext.Entry(existingService).State = EntityState.Modified;
            appContext.SaveChanges();
            return Ok(existingService);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "erreur lors du UpdateServiceById: {Message}", ex.Message);
            return StatusCode(500,$"Erreur interne du serveur: {ex.Message}");
        }
    }
}