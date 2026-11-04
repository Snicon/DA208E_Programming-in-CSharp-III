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
    
    /// <summary>
    /// Attempts to store the provided guest object in the database.
    /// </summary>
    /// <param name="guest">The new guest object to add to the database.</param>
    public async Task CreateGuestAsync(Guest guest)
    {
        _context.Guests.Add(guest);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Attempts to update the user of the given id with the data provided in the updatedGuest object
    /// </summary>
    /// <param name="id">The id of the guest to update</param>
    /// <param name="updatedGuest">The guest object containing the changes</param>
    /// <returns>True if successful, false if not.</returns>
    public async Task<bool> UpdateGuestAsync(int id, Guest updatedGuest)
    {
        var guest = await GetGuestByIdAsync(id);
        
        if (guest == null)
            return false;
        
        guest.Name = updatedGuest.Name;
        guest.Email = updatedGuest.Email;
        guest.PhoneNumber = updatedGuest.PhoneNumber;
        
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Attempts to delete the guest of the provided id from the database.
    /// </summary>
    /// <param name="id">The id of the guest to delete from the database</param>
    /// <returns>True if successful, false if not.</returns>
    public async Task<bool> DeleteGuestAsync(int id)
    {
        var guest = await GetGuestByIdAsync(id);

        if (guest == null)
            return false;

        _context.Guests.Remove(guest);
        await _context.SaveChangesAsync();
        return true;
    }
    
    /// <summary>
    /// Attempts to update the user of the given id with the data provided in the updatedGuest object
    /// This method includes not only basic user data but also RSVP-related user data which is how it differs
    /// from the "normal" update method.
    /// </summary>
    /// <param name="id">The id of the guest to update</param>
    /// <param name="updatedGuest">The guest object containing the changes</param>
    /// <returns>True if successful, false if not.</returns>
    public async Task<bool> UpdateGuestRsvpAsync(Guest updatedGuest)
    {
        var guest = await GetGuestByIdAsync(updatedGuest.Id);
        
        if (guest == null)
            return false;
        
        guest.Name = updatedGuest.Name;
        guest.Email = updatedGuest.Email;
        guest.PhoneNumber = updatedGuest.PhoneNumber;
        guest.NumberOfAttendants = updatedGuest.NumberOfAttendants;
        guest.Attending = updatedGuest.Attending;
        guest.Message = updatedGuest.Message;
        
        await _context.SaveChangesAsync();
        return true;
    }
}