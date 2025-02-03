using AnnuaireModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnnuaireAPI.Controllers;
[ApiController]
[Authorize]


public class SiteController : ControllerBase
{
    private readonly ILogger<SiteController> _logger;
    private readonly AppDbContext appContext;

    public SiteController(AppDbContext context, ILogger<SiteController> logger)
    {
        appContext = context;
        _logger = logger;
    }
    // Route API pour récupérer tous les sites
    [HttpGet("Get All [controller]", Name = "GetServices")]
    public ActionResult<IEnumerable<Service>> GetServices()
    {
        try
        {
            var site = appContext.Sites.ToList();
            if (site.Count == 0)
            {
                return NotFound(new {message = "No sites found"});
            }

            return Ok(site);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "erreur lors du get all sites: {Message}", ex.Message);
            return StatusCode(500,$"Erreur interne du serveur: {ex.Message}");
        }
    }
    // Route API pour ajouter un site
    [HttpPost("Create [controller]", Name = "AddSite")]
    public IActionResult PostSite(Site site)
    {
        try
        {
            if (site == null)
            {
                return BadRequest(new {message = "Invalid data"});
            }
            appContext.Sites.Add(site);
            appContext.SaveChanges();
            return CreatedAtRoute("GetSite", new { id = site.Id }, site);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "erreur lors de l'ajout d'un employe: {Message}", ex.Message);
            return StatusCode(500,$"Erreur interne du serveur: {ex.Message}");
        }
    }
    // Route API pour supprimer un site via son id
    [HttpDelete("Delete [controller]", Name = "DeleteSite")]
    public IActionResult DeleteSite(int id)
    {
        try
        {
            var site = appContext.Sites.FirstOrDefault(s => s.Id == id);
            if (site == null)
            {
                return NotFound(new {message = "No site found"});
            }
            appContext.Sites.Remove(site);
            appContext.SaveChanges();
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "erreur lors du DeleteSite: {Message}", ex.Message);
            return StatusCode(500,$"Erreur interne du serveur: {ex.Message}");
        }
    }
}