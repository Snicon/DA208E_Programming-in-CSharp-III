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

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var theEvent = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
        
        if (theEvent == null)
            return NotFound();
        
        return View(theEvent);
    }
    
    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var theEvent = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);

        if (theEvent == null)
            return NotFound();

        try
        {
            _context.Events.Remove(theEvent);
            await _context.SaveChangesAsync();

            TempData["MessageType"] = "success"; // Setting relevant message type, which is used for alerts in UI
            TempData["Message"] =
                "Event successfully deleted."; // Setting relevant message, which is used for the same as above
        }
        catch (DbUpdateException)
        {
            TempData["MessageType"] = "danger"; // Overwriting the MessageType since something went wrong
            TempData["Message"] = "Failed to delete event."; // Same as above
        }
        
        return RedirectToAction(nameof(List));
    }
}