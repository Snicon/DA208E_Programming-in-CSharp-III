/* Sixten Peterson (AQ9300) 2026-11-02 */

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
            Id = g.Id,
            Name = g.Name,
            Email = g.Email,
            Attending = g.Attending,
            NumberOfAttendants = g.NumberOfAttendants
        })
        .OrderBy(g => g.Name)
        .ToListAsync();
        
        return Ok(guests);
    }

    /// <summary>
    /// Retrieves data about the specified guest from the database and essentially uses the GuestViewModel as a DTO to
    /// expose a subset of guest data as JSON.
    /// </summary>
    /// <param name="id">The id of the guest to show data about</param>
    /// <returns>A guest object in JSON if guest was found, or 404 if not found.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult> GetGuest(int id)
    {
        var guest = await _context.Guests
            .Where(g => g.Id == id)
            .Select(g => new GuestViewModel
            {
                Id = g.Id,
                Name = g.Name,
            })
            .FirstOrDefaultAsync(g => g.Id == id);
        
        if (guest == null)
            return NotFound();
                
        return Ok(guest);
    }
}