using AnnuaireModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            if 
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}