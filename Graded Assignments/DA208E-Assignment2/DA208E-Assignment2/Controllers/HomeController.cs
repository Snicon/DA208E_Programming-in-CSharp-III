using System.Diagnostics;
using DA208E_Assignment2.Data;
using Microsoft.AspNetCore.Mvc;
using DA208E_Assignment2.Models;
using Microsoft.EntityFrameworkCore;

namespace DA208E_Assignment2.Controllers;

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
    
    public async Task<IActionResult> Index()
    {
        var information = await _context.UsefulInformation.ToListAsync();
        
        return View(information);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}