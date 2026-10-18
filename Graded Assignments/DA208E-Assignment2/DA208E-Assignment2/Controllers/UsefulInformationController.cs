// Sixten Peterson (AQ9300) 2026-10-18
using DA208E_Assignment2.Data;
using DA208E_Assignment2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DA208E_Assignment2.Controllers;

/// <summary>
/// A basic CRUD controller for managing useful information entries.
/// </summary>
public class UsefulInformationController : Controller
{
    #region Fields
    private readonly ApplicationDbContext _context;
    #endregion
    
    public UsefulInformationController(ApplicationDbContext context)
    {
        _context = context;
    }

    #region Controller action(s)
    /// <summary>
    /// The Create (get) controller action of the UsefulInformationController, a get request to the create page that
    /// shows a form for creating a new useful information entry.
    /// </summary>
    /// <returns>The create view</returns>
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    /// <summary>
    /// The Create (post) controller action of the UsefulInformationController, a post request to store the provided
    /// object in the database.
    /// </summary>
    /// <param name="usefulInformation">The new useful information object to store in the database</param>
    /// <returns>Redirection to Dashboard Index action if validation passed, create view if not</returns>
    [HttpPost]
    public async Task<IActionResult> Create(UsefulInformation usefulInformation)
    {
        if (!ModelState.IsValid) // Validation failed
            return View();

        _context.UsefulInformation.Add(usefulInformation);

        try
        {
            await _context.SaveChangesAsync();
            TempData["MessageType"] = "success"; // Setting relevant message type, which is used for alerts in UI
            TempData["Message"] =
                "Useful information entry successfully created."; // Setting relevant message, which is used for the same as above
        }
        catch (DbUpdateException)
        {
            TempData["MessageType"] = "danger"; // Overwriting the MessageType since something went wrong
            TempData["Message"] = "Failed to create useful information entry."; // Same as above
        }

        return RedirectToAction("Index", "Dashboard");
    }
    
    /// <summary>
    /// The Edit (get) controller action of the UsefulInformationController, a get request to the edit page that shows
    /// an edit form for editing an entry's details. The form is pre-filled with information.
    /// </summary>
    /// <param name="id">The id of the useful information entry to edit</param>
    /// <returns>Not found if no matching entry is found. Otherwise, useful information edit view along with the data
    /// needed to pre-fill it</returns>
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var information = await _context.UsefulInformation.FirstOrDefaultAsync(i => i.Id == id);
        
        if (information == null) // No matching entry found
            return NotFound();
        
        return View(information);
    }
    
    /// <summary>
    /// The Edit (post) controller action of the UsefulInformationController, a post request to store the provided
    /// useful information object in the database. Uses TempData to pass a message to the dashboard view with the status
    /// of the edit.
    /// </summary>
    /// <param name="id">The id of the useful information entry to update</param>
    /// <returns>Not found if no matching entry is found, otherwise redirection to dashboard index view</returns>
    [HttpPost]
    [ActionName("Edit")]
    public async Task<IActionResult> EditUsefulInformation(int id)
    {
        var information = await _context.UsefulInformation.FirstOrDefaultAsync(i => i.Id == id);

        if (information == null) // No matching entry found
            return NotFound();
        
        // For reference: https://learn.microsoft.com/en-us/aspnet/core/data/ef-mvc/crud?view=aspnetcore-10.0#update-the-edit-page
        if (await TryUpdateModelAsync<UsefulInformation>(
                information,
                "",
                i => i.Icon, i => i.Title, i => i.Description))
        {
            try
            {
                await _context.SaveChangesAsync();
                TempData["MessageType"] = "success"; // Setting relevant message type, which is used for alerts in UI
                TempData["Message"] = "Useful information entry successfully updated."; // Setting relevant message, which is used for the same as above
            }
            catch (DbUpdateException)
            {
                TempData["MessageType"] = "danger"; // Overwriting the MessageType since something went wrong
                TempData["Message"] = "Failed to update useful information entry."; // Same as above
            }
        }
        
        return RedirectToAction("Index", "Dashboard");
    }
    
    /// <summary>
    /// The Delete (get) controller action of the UsefulInformationController, a get request to the confirmation page to
    /// confirm the deletion of the entry. Said confirmation page also shows the data that will be deleted.
    /// </summary>
    /// <param name="id">The id of the useful information to delete</param>
    /// <returns>Not found if no matching entry is found, otherwise the delete view.</returns>
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var information = await _context.UsefulInformation.FirstOrDefaultAsync(i => i.Id == id);
        
        if (information == null) // No matching entry found
            return NotFound();
        
        return View(information);
    }
    
    /// <summary>
    /// The Delete (post) controller action of the UsefulInformationController, a post request to delete the entry with
    /// the provided id from the database. Uses TempData to pass a message to the dashboard view with the status of
    /// deletion.
    /// </summary>
    /// <param name="id">The id of the entry to delete from the database</param>
    /// <returns>Not found if no matching entry is found. Otherwise, redirection to the index action of the dashboard
    /// controller</returns>
    [HttpPost]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var information = await _context.UsefulInformation.FirstOrDefaultAsync(i => i.Id == id);

        if (information == null)
            return NotFound();

        try
        {
            _context.UsefulInformation.Remove(information);
            await _context.SaveChangesAsync();
            
            TempData["MessageType"] = "success"; // Setting relevant message type, which is used for alerts in UI
            TempData["Message"] = "Useful information entry successfully deleted."; // Setting relevant message, which is used for the same as above
        }
        catch (DbUpdateException)
        {
            TempData["MessageType"] = "danger"; // Overwriting the MessageType since something went wrong
            TempData["Message"] = "Failed to delete useful information entry."; // Same as above
        }
        
        return RedirectToAction("Index", "Dashboard");
    }
    #endregion
}