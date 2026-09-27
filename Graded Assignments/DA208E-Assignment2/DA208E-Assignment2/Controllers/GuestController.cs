using DA208E_Assignment2.Data;
using DA208E_Assignment2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DA208E_Assignment2.Controllers;

public class GuestController : Controller
{
    #region Fields
    private readonly ApplicationDbContext _context;
    private readonly Event _eventInfo;
    #endregion
    
    public GuestController(ApplicationDbContext context, Event eventInfo)
    {
        _context = context;
        _eventInfo = eventInfo;
    }
    
    public async Task<IActionResult> Index()
    {
        var guests = await _context.Guests.ToListAsync();
        
        return View(guests);
    }
    
    // GET: /Guest/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    // POST: /Guest/Create
    [HttpPost]
    public async Task<IActionResult> Create(Guest guest)
    {
        // The follwoing properties are not used in the create form, hence we are removing them form the validation
        ModelState.Remove("NumberOfAttendants");
        ModelState.Remove("Attending");
        ModelState.Remove("Message");
        
        if (!ModelState.IsValid)
        {
            return View();
        }
        
        _context.Guests.Add(guest);
        await _context.SaveChangesAsync();
        
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Rsvp(int id)
    {
        var guest = await _context.Guests.FirstOrDefaultAsync(g => g.Id == id);
        
        return View(guest);
    }

    [HttpPost]
    public async Task<IActionResult> Rsvp(Guest model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var guest = await _context.Guests.FirstOrDefaultAsync(g => g.Id == model.Id);

        if (guest == null)
            return NotFound();
        
        guest.Attending = model.Attending;
        guest.Message = model.Message;

        await _context.SaveChangesAsync();
        
        return RedirectToAction(nameof(Confirmation), new { id = guest.Id});
    }

    [HttpGet]
    public async Task<IActionResult> Confirmation(int id)
    {
        var guest = await _context.Guests.FirstOrDefaultAsync(g => g.Id == id);

        if (guest == null || _eventInfo == null)
            return NotFound();

        var model = new ConfirmationViewModel
        {
            Guest = guest,
            EventInfo = _eventInfo
        };

    return View(model);
    }
}