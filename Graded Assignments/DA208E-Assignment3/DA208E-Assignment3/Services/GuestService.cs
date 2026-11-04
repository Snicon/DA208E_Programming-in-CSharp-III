using DA208E_Assignment3.Data;
using DA208E_Assignment3.Models;
using Microsoft.EntityFrameworkCore;

namespace DA208E_Assignment3.Services;

public class GuestService
{
    private readonly ApplicationDbContext _context;

    public GuestService(ApplicationDbContext context)
    {
        _context = context;   
    }

    public async Task<List<Guest>> GetAllGuestsAsync()
    {
        return await _context.Guests.ToListAsync();
    }

    public async Task<Guest?> GetGuestByIdAsync(int id)
    {
        return await _context.Guests.FindAsync(id);
    }
}