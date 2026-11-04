/* Sixten Peterson (AQ9300) 2026-11-04 */
using DA208E_Assignment3.Data;
using DA208E_Assignment3.Models;
using Microsoft.EntityFrameworkCore;

namespace DA208E_Assignment3.Services;

/// <summary>
/// Simple service class based on the example on p.6 in Assignment 3 - Help document.
/// </summary>
public class GuestService
{
    private readonly ApplicationDbContext _context;

    public GuestService(ApplicationDbContext context)
    {
        _context = context;   
    }

    /// <summary>
    /// Gets all guests from the database and returns them in a list.
    /// </summary>
    /// <returns>List of guests from database</returns>
    public async Task<List<Guest>> GetAllGuestsAsync()
    {
        return await _context.Guests.ToListAsync();
    }

    /// <summary>
    /// Attempts to get the user of the given ID from the database
    /// </summary>
    /// <param name="id">The id of the user to get from the database</param>
    /// <returns>Guest object with data from DB if successful, null if not found.</returns>
    public async Task<Guest?> GetGuestByIdAsync(int id)
    {
        return await _context.Guests.FindAsync(id);
    }
}