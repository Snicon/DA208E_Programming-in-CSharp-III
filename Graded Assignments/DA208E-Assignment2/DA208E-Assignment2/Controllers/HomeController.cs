// Sixten Peterson (AQ9300) 2026-10-18
using System.Diagnostics;
using DA208E_Assignment2.Data;
using Microsoft.AspNetCore.Mvc;
using DA208E_Assignment2.Models;
using Microsoft.EntityFrameworkCore;

namespace DA208E_Assignment2.Controllers;

/// <summary>
/// A basic controller for the home page, essentially houses an index action for the home page.
/// </summary>
public class HomeController : Controller
{
    #region Fields
    private readonly ApplicationDbContext _context;
    #endregion
    
    #region Constructors

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }
    #endregion

    #region Controller action(s)
    /// <summary>
    /// Simple index controller action, fetches useful information entries from the database and returns it within the
    /// index view.
    /// </summary>
    /// <returns>The index view along with the useful information data</returns>
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var information = await _context.UsefulInformation.ToListAsync();
        
        return View(information);
    }

    // This was here from scaffolding the project
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    #endregion
}