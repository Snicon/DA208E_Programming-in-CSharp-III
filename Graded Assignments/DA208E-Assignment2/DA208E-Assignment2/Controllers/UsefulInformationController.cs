using DA208E_Assignment2.Data;
using DA208E_Assignment2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DA208E_Assignment2.Controllers;

public class UsefulInformationController : Controller
{
    #region Fields
    private readonly ApplicationDbContext _context;
    #endregion
    
    public UsefulInformationController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(UsefulInformation usefulInformation)
    {
        if (!ModelState.IsValid)
            return View();

        _context.UsefulInformation.Add(usefulInformation);
        await _context.SaveChangesAsync();
        
        return RedirectToAction("Index", "Dashboard");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var information = await _context.UsefulInformation.FirstOrDefaultAsync(i => i.Id == id);
        
        if (information == null)
            return NotFound();
        
        return View(information);
    }

    [HttpPost]
    public async Task<IActionResult> EditUsefulInformation(int id)
    {
        var information = await _context.UsefulInformation.FirstOrDefaultAsync(i => i.Id == id);

        if (information == null)
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

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var information = await _context.UsefulInformation.FirstOrDefaultAsync(i => i.Id == id);
        
        if (information == null)
            return NotFound();
        
        return View(information);
    }

    [HttpPost]
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
}