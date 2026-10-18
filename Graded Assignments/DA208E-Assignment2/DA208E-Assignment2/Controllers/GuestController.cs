// Sixten Peterson (AQ9300) 2026-10-18
using DA208E_Assignment2.Data;
using DA208E_Assignment2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DA208E_Assignment2.Controllers;

/// <summary>
/// A basic CRUD controller for managing guests. It also includes additional actions for RSVP logic and a confirmation
/// view.
/// </summary>
public class GuestController : Controller
{
    #region Fields
    private readonly ApplicationDbContext _context;
    #endregion

    #region Constructors
    public GuestController(ApplicationDbContext context)
    {
        _context = context;
    }
    #endregion
    
    #region Controller action(s)
    /// <summary>
    /// The Index controller action of the GuestController, a get request to the index page that showcases selected
    /// guest data (full guest data is shown in the dashboard).
    /// </summary>
    /// <returns>The guest index view along with the view model.</returns>
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var guests = await _context.Guests
            .OrderBy(g => g.Name) // Sorting by name
            .ToListAsync();
        
        return View(guests);
    }
    
    /// <summary>
    /// The Details (get) controller action of the GuestController, a get request to the details page of a guest that
    /// shows details about the specified guest.
    /// </summary>
    /// <param name="id">The id of the guest</param>
    /// <returns>Not found if no matching guest is found, otherwise the details view along with the guest data</returns>
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var guest = await _context.Guests.FirstOrDefaultAsync(g => g.Id == id);
        
        if (guest == null)
            return NotFound();
        
        return View(guest);
    }
    
    /// <summary>
    /// The Create (get) controller action of the GuestController, a get request to the create page that shows a form
    /// for creating a new guest.
    /// </summary>
    /// <returns>The guest create view</returns>
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    /// <summary>
    /// The Create (post) controller action of the GuestController, a post request to store the provided guest object in
    /// the database.
    /// </summary>
    /// <param name="guest">The new guest to store in the database</param>
    /// <returns>Redirection to guest Index action if validation passed, create view if not</returns>
    [HttpPost]
    public async Task<IActionResult> Create(Guest guest)
    {
        // The following properties are not used in the create form, hence why they are removed them from the validation
        ModelState.Remove("NumberOfAttendants");
        ModelState.Remove("Attending");
        ModelState.Remove("Message");
        
        if (!ModelState.IsValid) // Validation failed -> returning the create view again
        {
            return View();
        }
        
        _context.Guests.Add(guest);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        { }

        return RedirectToAction("Index");
    }

    /// <summary>
    /// The Edit (get) controller action of the EventController, a get request to the edit page that shows an edit
    /// form for editing a guest's details. The form is pre-filled with information.
    /// </summary>
    /// <param name="id">The id of the guest to edit</param>
    /// <returns>Not found if no matching guest is found. Otherwise, the guest edit view along with the data needed to
    /// pre-fill it</returns>
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var guest = await _context.Guests.FirstOrDefaultAsync(g => g.Id == id);
        
        if(guest == null) // No matching guest found
            return NotFound();
        
        return View(guest);
    }

    /// <summary>
    /// The Edit (post) controller action of the GuestController, a post request to store the provided guest object
    /// in the database. Uses TempData to pass a message to the dashboard view with the status of the edit.
    /// </summary>
    /// <param name="id">The id of the guest to update</param>
    /// <returns>Not found if no matching event is found, otherwise redirection to dashboard index view</returns>
    [HttpPost]
    [ActionName("Edit")]
    public async Task<IActionResult> EditGuest(int id)
    {
        var guest = await _context.Guests.FirstOrDefaultAsync(g => g.Id == id);
        
        if (guest == null) // No matching guest found
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
        
        return RedirectToAction("Index", "Dashboard");
    }

    /// <summary>
    /// The Delete (get) controller action of the GuestController, a get request to the confirmation page to confirm the
    /// deletion of the guest. Said confirmation page also shows the data that will be deleted.
    /// </summary>
    /// <param name="id">The id of the guest to delete</param>
    /// <returns>Not found if no matching guest is found, otherwise the delete view.</returns>
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var guest = await _context.Guests.FirstOrDefaultAsync(g => g.Id == id);
        
        if (guest == null) // No matching guest found
            return NotFound();
        
        return View(guest);
    }
    
    /// <summary>
    /// The Delete (post) controller action of the GuestController, a post request to delete the guest with the provided
    /// id from the database. Uses TempData to pass a message to the dashboard view with the status of deletion.
    /// </summary>
    /// <param name="id">The id of the guest to delete from the database</param>
    /// <returns>Not found if no matching guest is found. Otherwise, redirection to the index action of the dashboard
    /// controller</returns>
    [HttpPost]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var guest = await _context.Guests.FirstOrDefaultAsync(g => g.Id == id);

        if (guest == null) // No matching guest found
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
        
        return RedirectToAction("Index", "Dashboard");
    }

    #region Beyond simple CRUD
    /// <summary>
    /// The Rsvp (get) controller action of the GuestController, a get request that shows the RSVP entry form (pre-fills
    /// some information).
    /// </summary>
    /// <param name="id">The id of the guest that is having their RSVP form filled</param>
    /// <returns>Not found if no matching event is found, otherwise the Rsvp view along with the guest data.</returns>
    [HttpGet]
    public async Task<IActionResult> Rsvp(int id)
    {
        var guest = await _context.Guests.FirstOrDefaultAsync(g => g.Id == id);

        if (guest == null) // No matching guest found
            return NotFound();
        
        return View(guest);
    }

    /// <summary>
    /// The Rsvp (post) action of the GuestController, a post request to store the provided guest object in the
    /// database.
    /// </summary>
    /// <param name="model">The guest object of data to store</param>
    /// <returns>Not found if no matching guest is found, otherwise a redirection to the confirmation action</returns>
    [HttpPost]
    public async Task<IActionResult> Rsvp(Guest model)
    {
        if (!ModelState.IsValid) // Validation failed
            return View(model);

        var guest = await _context.Guests.FirstOrDefaultAsync(g => g.Id == model.Id);

        if (guest == null) // No matching guest found
            return NotFound();
        
        guest.Name = model.Name;
        guest.Email = model.Email;
        guest.PhoneNumber = model.PhoneNumber;
        guest.NumberOfAttendants = model.NumberOfAttendants;
        guest.Attending = model.Attending;
        guest.Message = model.Message;

        await _context.SaveChangesAsync();
        
        return RedirectToAction("Confirmation", "Guest", new { id = guest.Id });
    }
    
    /// <summary>
    /// The Confirmation controller action of the GuestController, a get request to the confirmation page to view the
    /// confirmation of the RSVP.
    /// </summary>
    /// <param name="id">The id of the guest the confirmation page is shown for.</param>
    /// <returns>Not found if no matching event is found, otherwise the confirmation view</returns>
    [HttpGet]
    public async Task<IActionResult> Confirmation(int id)
    {
        var guest = await _context.Guests.FirstOrDefaultAsync(g => g.Id == id);

        if (guest == null) // No matching guest found
            return NotFound();

        var model = new ConfirmationViewModel
        {
            Guest = guest,
        };

        return View(model);
    }
    #endregion
    #endregion
}