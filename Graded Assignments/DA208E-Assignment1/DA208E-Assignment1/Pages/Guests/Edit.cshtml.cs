using DA208E_Assignment1.Data;
using DA208E_Assignment1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DA208E_Assignment1.Pages.Guests;

/// <summary>
/// The code-behind for the Edit page for managing guests. Uses dependency injection to access guest service.
/// </summary>
public class EditModel : PageModel
{
    #region Fields
    private GuestService _guestService;
    #endregion

    #region Properties
    [BindProperty]
    public Guest Guest { get; set; }
    #endregion

    #region Constructors
    public EditModel(GuestService guestService)
    {
        _guestService = guestService;
    }
    #endregion

    #region Methods
    /// <summary>
    /// Handles the HTTP GET request for this page. Attempts to get the provided guest before showing page, redirects to Index if no matching guest was found.
    /// </summary>
    public IActionResult OnGet(int id)
    {
        Guest = _guestService.Get(id); // Getting user by id

        if (Guest == null) // Failed to get a guest
            return RedirectToPage("Index"); // Redirects to index
        
        return Page(); // Shows the page
    }
    
    /// <summary>
    /// Updates guest from the submitted rsvp edit form data. Passes a message via TempData reflecting the status. If validation fails the page is shown again, otherwise redirects to index with the tempdata.
    /// </summary>
    /// <returns>Either the page or a page redirect depending on whether validation passes.</returns>
    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) // Validation check, failed
            return Page();
        
        bool updateSucceeded = _guestService.Update(Guest); // Attempting to update the guests data
        
        TempData["MessageType"] = updateSucceeded ? "success" : "danger"; // Setting relevant message type, which is used for alerts in UI
        TempData["Message"] = updateSucceeded
            ? "Guest successfully updated."
            : "Failed to update guest."; // Setting relevant message, which is used for the same as above
        
        return RedirectToPage("Index"); // Redirecting to index
    }
    #endregion
}