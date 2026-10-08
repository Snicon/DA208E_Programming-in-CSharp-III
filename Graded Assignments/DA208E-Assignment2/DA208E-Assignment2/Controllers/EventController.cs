using DA208E_Assignment2.Data;
using DA208E_Assignment2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace DA208E_Assignment2.Controllers;

public class EventController : Controller
{
    #region Fields
    private readonly ApplicationDbContext _context;
    #endregion
    
    #region Constructors

    public EventController(ApplicationDbContext context)
    {
        _context = context;
    }
    #endregion
    
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var events = await _context.Events.ToListAsync();
        return View(events);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(Event newEvent)
    {
        if (!ModelState.IsValid)
            return View();

        _context.Events.Add(newEvent);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(List));
    }
}