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
        var model = new GuestIndexViewModel()
        {
            Guests = await _context.Guests
                .OrderBy(g => g.Name) // Sorting by name
                .ToListAsync(),
            AttendingGuests = await _context.Guests
                .Where(g => g.Attending == true)
                .OrderBy(g => g.Name) // Sorting by name
                .ToListAsync(),
            NotAttendingGuests = await _context.Guests
                .Where(g => g.Attending == false)
                .OrderBy(g => g.Name) // Sorting by name
                .ToListAsync()
        };
        
        return View(model);
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
        
        guest.NumberOfAttendants = model.NumberOfAttendants;
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

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var guest = await _context.Guests.FirstOrDefaultAsync(g => g.Id == id);
        
        if (guest == null)
            return NotFound();
        
        return View(guest);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var guest = await _context.Guests.FirstOrDefaultAsync(g => g.Id == id);
        
        if(guest == null)
            return NotFound();
        
        return View(guest);
    }

    [HttpPost]
    public async Task<IActionResult> EditGuest(int id)
    {
        var guest = await _context.Guests.FirstOrDefaultAsync(g => g.Id == id);
        
        if (guest == null)
            return NotFound();

        // For reference: https://learn.microsoft.com/en-us/aspnet/core/data/ef-mvc/crud?view=aspnetcore-10.0#update-the-edit-page
        if (await TryUpdateModelAsync<Guest>(
                guest,
                "",
                g => g.Name, g => g.Email, g => g.PhoneNumber))
        {
            try
            {
                await _context.SaveChangesAsync();
                TempData["MessageType"] = "success"; // Setting relevant message type, which is used for alerts in UI
                TempData["Message"] = "Guest successfully updated."; // Setting relevant message, which is used for the same as above
            }
            catch (DbUpdateException)
            {
                TempData["MessageType"] = "danger"; // Overwriting the MessageType since something went wrong
                TempData["Message"] = "Failed to update guest."; // Same as above
            }
        }
        
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var guest = await _context.Guests.FirstOrDefaultAsync(g => g.Id == id);
        
        if (guest == null)
            return NotFound();
        
        return View(guest);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var guest = await _context.Guests.FirstOrDefaultAsync(g => g.Id == id);

        if (guest == null)
            return NotFound();

        try
        {
            _context.Guests.Remove(guest);
            await _context.SaveChangesAsync();
            
            TempData["MessageType"] = "success"; // Setting relevant message type, which is used for alerts in UI
            TempData["Message"] = "Guest successfully deleted."; // Setting relevant message, which is used for the same as above
        }
        catch (DbUpdateException)
        {
            TempData["MessageType"] = "danger"; // Overwriting the MessageType since something went wrong
            TempData["Message"] = "Failed to delete guest."; // Same as above
        }
        
        return RedirectToAction(nameof(Index));
    }
}