using DA208E_Assignment2.Data;
using DA208E_Assignment2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DA208E_Assignment2.Controllers;

public class DashboardController : Controller
{
    #region Fields
    private readonly ApplicationDbContext _context;
    #endregion

    public DashboardController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    // GET
    public async Task<IActionResult> Index()
    {
        var model = new DashboardViewModel()
        {
            RegisteredGuests = await _context.Guests.CountAsync(),
            AttendingGuests = await _context.Guests
                .Where(g => g.Attending == true)
                .CountAsync(),
            NonAttendingGuests = await _context.Guests
                .Where(g => g.Attending == false)
                .CountAsync(),
            Attendants = await _context.Guests
                .Where(g => g.Attending == true)
                .SumAsync(g => g.NumberOfAttendants ?? 0) // ?? 0 is used to treat null as 0
        };
        
        return View(model);
    }
}