// Sixten Peterson (AQ9300) 2026-09-14
using DA208E_Assignment1.Models;

namespace DA208E_Assignment1.Data;

/// <summary>
/// GuestService is a service used for storing Guest data in-memory. It handles various CRUD actions for storing guest data.
/// </summary>
public class GuestService : IDataService<Guest>
{
    #region Fields
    private readonly List<Guest> _guests = new(); // Field for storing list of all guests
    private int _nextId = 1; // Used for assigning unique id's
    #endregion
    
    #region Methods
    /// <summary>
    /// Gets a list of all guests stored in the service.
    /// </summary>
    /// <returns>list of all guests</returns>
    public List<Guest> GetAll() => _guests;
    
    /// <summary>
    /// Gets the first guest with the specified id from the guests list
    /// </summary>
    /// <param name="id">The id of the guest to get</param>
    /// <returns>The guest if found</returns>
    public Guest? Get(int id) => _guests.FirstOrDefault(g => g.Id == id);

    /// <summary>
    /// Attempts to add a new guest
    /// </summary>
    /// <param name="guest">The guest object to add</param>
    public void Add(Guest guest)
    {
        guest.Id = _nextId++; // Upping the nextId to make sure every guest gets a unique id
        _guests.Add(guest);
    }

    /// <summary>
    /// Attempts to update the specified guest with new Guest data
    /// </summary>
    /// <param name="guest">The new guest data</param>
    /// <returns>True if successful, false if unsuccessful in updating.</returns>
    public bool Update(Guest guest)
    {
        var index = _guests.FindIndex(g => g.Id == guest.Id); // Trying to find index of guest
        if (index == -1) // Index not found for guest
            return false;
        
        _guests[index] = guest; // Update guest data
        return true; // Update stored in list
    }

    /// <summary>
    /// Attempts ot delete the specified guest
    /// </summary>
    /// <param name="id">The id of the guest to delete</param>
    /// <returns>True if successful, false if unsuccessful in deleting.</returns>
    public bool Delete(int id)
    {
        var guest = Get(id); // Gets the guest based on id
        
        if (guest == null)
            return false; // Failed to find a guest matching the id
        
        return _guests.Remove(guest); // Attempting to remove guest from list
    }
    #endregion
}