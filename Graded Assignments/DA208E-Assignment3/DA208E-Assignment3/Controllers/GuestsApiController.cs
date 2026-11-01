/* Sixten Peterson (AQ9300) 2026-11-01 */

using DA208E_Assignment3.Data;
using DA208E_Assignment3.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DA208E_Assignment3.Controllers;

/// <summary>
/// Simple API controller used to expose guest data over a JSON REST API.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class GuestsApiController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public GuestsApiController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// Retrieves all guests from the database and essentially uses the GuestViewModel as a DTO to expose a subset of
    /// guest data as JSON.
    /// </summary>
    /// <returns>A list of guests containing data such as: name, email, attending and number of attendants.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GuestViewModel>>> GetGuests()
    {
        var guests = await _context.Guests.Select(g => new GuestViewModel
        {
            Name = g.Name,
            Email = g.Email,
            Attending = g.Attending,
            NumberOfAttendants = g.NumberOfAttendants
        }).ToListAsync();
        
        return Ok(guests);
    }
}