/* Sixten Peterson (AQ9300) 2026-11-02 */

using DA208E_Assignment3.Data;
using DA208E_Assignment3.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DA208E_Assignment3.Controllers;

/// <summary>
/// Simple API controller used to expose event data over a JSON REST API.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class EventsApiController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public EventsApiController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EventViewModel>>> GetEvents()
    {
        var events = await _context.Events.Select(e => new EventViewModel
        {
            Title = e.Title,
            Description = e.Description,
            DateTime = e.DateTime
        })
        .OrderBy(e => e.DateTime)
        .ToListAsync();
        
        return Ok(events);
    }
}