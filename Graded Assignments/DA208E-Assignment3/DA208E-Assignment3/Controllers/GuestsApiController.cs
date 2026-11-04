/* Sixten Peterson (AQ9300) 2026-11-02 */

using DA208E_Assignment3.Services;
using DA208E_Assignment3.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DA208E_Assignment3.Controllers;

/// <summary>
/// Simple API controller used to expose guest data over a JSON REST API.
/// </summary>
[Route("api/guests")]
[ApiController]
public class GuestsApiController : ControllerBase
{
    private readonly GuestService _service;
    
    public GuestsApiController(GuestService service)
    {
        _service = service;
    }
    
    /// <summary>
    /// Retrieves all guests from the database and essentially uses the GuestViewModel as a DTO to expose a subset of
    /// guest data as JSON.
    /// </summary>
    /// <returns>A list of guests containing data such as: name, email, attending and number of attendants.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GuestViewModel>>> GetGuests()
    {
        var guests = await _service.GetAllGuestsAsync();
        
        var viewModel = guests.Select(g => new GuestViewModel
        {
            Id = g.Id,
            Name = g.Name,
            Email = g.Email,
            Attending = g.Attending,
            NumberOfAttendants = g.NumberOfAttendants
        }).OrderBy(g => g.Name).ToList();
        
        return Ok(viewModel);
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
        var guest = await _service.GetGuestByIdAsync(id);
           
        if (guest == null)
            return NotFound();

        var viewModel = new GuestViewModel
        {
            Id = guest.Id,
            Name = guest.Name,
            Email = guest.Email,
            Attending = guest.Attending,
            NumberOfAttendants = guest.NumberOfAttendants
        };
                
        return Ok(viewModel);
    }
}