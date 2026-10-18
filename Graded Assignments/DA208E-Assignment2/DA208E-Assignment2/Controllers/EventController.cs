// Sixten Peterson (AQ9300) 2026-10-18
using DA208E_Assignment2.Data;
using DA208E_Assignment2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DA208E_Assignment2.Controllers;

/// <summary>
/// A basic CRUD controller for managing events.
/// </summary>
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

    #region Controller action(s)
    /// <summary>
    /// The Index controller action of the EventController, a get request to the index page that showcases event
    /// information.
    /// </summary>
    /// <returns>The event index view along with a list of event data ordered by DateTime</returns>
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var events = await _context.Events.OrderBy(e => e.DateTime).ToListAsync();
        
        return View(events);
    }
    
    /// <summary>
    /// The Create (get) controller action of the EventController, a get request to the create page that shows a form
    /// for creating a new event.
    /// </summary>
    /// <returns>The event create view</returns>
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    /// <summary>
    /// The Create (post) controller action of the EventController, a post request to store the provided event object in
    /// the database. Uses TempData to pass a message to the dashboard view with the status of creation.
    /// </summary>
    /// <param name="newEvent">The new event to store in the database</param>
    /// <returns>Redirection to Dashboard Index action if validation passed, create view if not</returns>
    [HttpPost]
    public async Task<IActionResult> Create(Event newEvent)
    {
        if (!ModelState.IsValid) // If something didn't validate the user is returned to the create view
            return View();
        
        _context.Events.Add(newEvent);
        
        try
        {
            await _context.SaveChangesAsync();
            TempData["MessageType"] = "success"; // Setting relevant message type, which is used for alerts in UI
            TempData["Message"] =
                "Event successfully created."; // Setting relevant message, which is used for the same as above
        }
        catch (DbUpdateException)
        {
            TempData["MessageType"] = "danger"; // Overwriting the MessageType since something went wrong
            TempData["Message"] = "Failed to create event."; // Same as above
        }

        return RedirectToAction("Index", "Dashboard");
    }
    
    /// <summary>
    /// The Edit (get) controller action of the EventController, a get request to the edit page that shows an edit
    /// form for editing an event's details. The form is pre-filled with information.
    /// </summary>
    /// <param name="id">The id of the event to edit</param>
    /// <returns>Not found if no matching event is found. Otherwise, event edit view along with the data needed to
    /// pre-fill it</returns>
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var theEvent = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
        
        if (theEvent == null) // Event with the specified id not found in DB
            return NotFound();
        
        return View(theEvent);
    }
    
    /// <summary>
    /// The Edit (post) controller action of the EventController, a post request to store the provided event object
    /// in the database. Uses TempData to pass a message to the dashboard view with the status of the edit.
    /// </summary>
    /// <param name="id">The id of the event to update</param>
    /// <returns>Not found if no matching event is found, otherwise redirection to dashboard index view</returns>
    [HttpPost]
    [ActionName("Edit")]
    public async Task<IActionResult> EditEvent(int id)
    {
        // evnet is a keyword, which made me use theEvent instead for var name
        var theEvent = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
        
        if (theEvent == null) // No event found by that id
            return NotFound();

        // For reference: https://learn.microsoft.com/en-us/aspnet/core/data/ef-mvc/crud?view=aspnetcore-10.0#update-the-edit-page
        if (await TryUpdateModelAsync<Event>(
                theEvent,
                "",
                e => e.Title, e => e.DateTime, e => e.Description))
        {
            try
            {
                await _context.SaveChangesAsync();
                TempData["MessageType"] = "success"; // Setting relevant message type, which is used for alerts in UI
                TempData["Message"] =
                    "Event successfully updated."; // Setting relevant message, which is used for the same as above
            }
            catch (DbUpdateException)
            {
                TempData["MessageType"] = "danger"; // Overwriting the MessageType since something went wrong
                TempData["Message"] = "Failed to update event."; // Same as above
            }
        }
        
        return RedirectToAction("Index", "Dashboard");
    }
    /// <summary>
    /// The Delete (get) controller action of the EventController, a get request to the confirmation page to confirm the
    /// deletion of the event. Said confirmation page also shows the data that will be deleted.
    /// </summary>
    /// <param name="id">The id of the event to delete</param>
    /// <returns>Not found if no matching event is found, otherwise the delete view.</returns>
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var theEvent = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
        
        if (theEvent == null) // No matching event found
            return NotFound();
        
        return View(theEvent);
    }
    
    /// <summary>
    /// The Delete (post) controller action of the EventController, a post request to delete the event with the provided
    /// id from the database. Uses TempData to pass a message to the dashboard view with the status of deletion.
    /// </summary>
    /// <param name="id">The id of the event to delete from the database</param>
    /// <returns>Not found if no matching event is found. Otherwise, redirection to the index action of the dashboard
    /// controller</returns>
    [HttpPost]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var theEvent = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);

        if (theEvent == null) // No matching event found
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
        
        return RedirectToAction("Index", "Dashboard");
    }
    #endregion
}