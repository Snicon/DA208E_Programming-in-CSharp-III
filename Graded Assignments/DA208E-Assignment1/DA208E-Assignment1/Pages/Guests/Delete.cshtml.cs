// Sixten Peterson (AQ9300) 2026-09-14
using DA208E_Assignment1.Data;
using DA208E_Assignment1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DA208E_Assignment1.Pages.Guests;

/// <summary>
/// The code-behind for the delete page. Uses dependency injection to access guest service which is used to delete the specified guest.
/// </summary>
public class Delete : PageModel
{
    #region Fields
    private GuestService _guestService;
    #endregion
    
    #region Properties
    [BindProperty]
    public Guest? Guest { get; set; }
    #endregion

    #region Constructors
    public Delete(GuestService guestService)
    {
        _guestService = guestService;
    }
    #endregion
    
    #region Methods
    /// <summary>
    /// Attempts to get the guest with the provided id; redirects to index if unable. If successful a deletion confirmation page is shown.
    /// </summary>
    /// <param name="id">The id of the guest to delete</param>
    /// <returns></returns>
    public IActionResult OnGet(int id)
    {
        Guest = _guestService.Get(id); // Attempting to get guest
        
        if (Guest == null) // Failed to find guest
            return RedirectToPage("Index"); // Redirecting to index
        
        return Page(); // Showing the page
    }

    /// <summary>
    /// Attempts to delete the guest, uses tempdata to pass a message with status to the index page.
    /// </summary>
    /// <returns>Redirect to index page</returns>
    public IActionResult OnPost()
    {
        if (Guest != null) // Making sure a guest was provided
        {
            bool deletionSucceeded = _guestService.Delete(Guest.Id); // Attempting to delete the given guest
            
            TempData["MessageType"] = deletionSucceeded ? "success" : "danger"; // Setting relevant message type, which is used for alerts in UI
            TempData["Message"] = deletionSucceeded
                ? "Guest successfully deleted."
                : "Failed to delete guest."; // Setting relevant message, which is used for the same as above
        }
        
        return RedirectToPage("Index"); // Redirecting to index
    }
    #endregion
}