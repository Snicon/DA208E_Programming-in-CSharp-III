// Sixten Peterson (AQ9300) 2026-10-18

using DA208E_Assignment3.Data;
using DA208E_Assignment3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DA208E_Assignment3.Controllers;

/// <summary>
/// A basic controller class for the dashboard, consisting of a simple index action. It uses the database to fetch
/// relevant data.
/// </summary>
[Authorize]// Note to self: p.597 in ASP.NET Core In Action (Third edition) by Andrew Lock, p 6 in Assignment 3 document
public class DashboardController : Controller
{
    #region Fields
    private readonly ApplicationDbContext _context;
    #endregion

    public DashboardController(ApplicationDbContext context)
    {
        _context = context;
    }

    #region Controller action(s)
    /// <summary>
    /// The Index controller action of the dashboard, a get request to the dashboard page (index) that fetches the
    /// relevant data from the database and "compiles" it into the DashboardViewModel and returns the view along with
    /// the view model.
    /// </summary>
    /// <returns>The dashboard index view along with the view model</returns>
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var model = new DashboardViewModel()
        {
            RegisteredGuests = await _context.Guests.ToListAsync(),
            AttendingGuests = await _context.Guests
                .Where(g => g.Attending == true)
                .ToListAsync(),
            NonAttendingGuests = await _context.Guests
                .Where(g => g.Attending == false)
                .ToListAsync(),
            Attendants = await _context.Guests
                .Where(g => g.Attending == true)
                .SumAsync(g => g.NumberOfAttendants ?? 0), // ?? 0 is used to treat null as 0
            Events = await _context.Events.ToListAsync(),
            UsefulInformation = await _context.UsefulInformation.ToListAsync()
        };
        
        return View(model);
    }
    #endregion
}