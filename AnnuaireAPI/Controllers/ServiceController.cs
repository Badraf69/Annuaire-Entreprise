using AnnuaireModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnnuaireAPI.Controllers;
[ApiController]
[Authorize]
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
    [HttpGet("Get all [controller]", Name = "GetAllServices")]
    public ActionResult<IEnumerable<Service>> GetServices()
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
    [HttpPost("api/[controller]", Name = "PostServiceById")]
    public IActionResult PostService(Service service)
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
    [HttpDelete("Delete [controller] by Id/{id}", Name = "DeleteServiceById")]
    public IActionResult DeleteService(int id)
    {
        try
        {
            var service = appContext.Services
                .Find(id);
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
            _logger.LogError(ex, "erreur lors du DeleteService: {Message}", ex.Message);
            return StatusCode(500,$"Erreur interne du serveur: {ex.Message}");
        }
    }
    //Route API pour modifier un service via son id
    [HttpPut("Update [controller] by Id/{id}", Name = "UpdateServiceById")]
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