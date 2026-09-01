using DA208E_Assignment1.Models;

namespace DA208E_Assignment1.Data;

public class GuestService : IDataService<Guest>
{
    #region Fields

    private readonly List<Guest> _guests = new(); // Field for storing list of all guests
    private int _nextId = 1; // Used for assigning unique id's
    #endregion
    
    #region Methods
    public List<Guest> GetAll() => _guests;
    
    public Guest? Get(int id) => _guests.FirstOrDefault(g => g.Id == id);

    public void Add(Guest guest)
    {
        guest.Id = _nextId++;
        _guests.Add(guest);
    }

    public bool Update(Guest guest)
    {
        var index = _guests.FindIndex(g => g.Id == guest.Id);
        if (index == -1) // Index not found for guest
            return false;
        
        _guests[index] = guest;
        return true; // Update stored in list
    }

    public bool Delete(int id)
    {
        var guest = Get(id);
        
        if (guest == null)
            return false; // Failed to find a guest matching the id
        
        return _guests.Remove(guest); // Attempting to remove guest from list
    }
    #endregion
}