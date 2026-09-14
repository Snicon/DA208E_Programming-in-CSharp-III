// Sixten Peterson (AQ9300) 2026-09-14
using DA208E_Assignment1.Data;
using DA208E_Assignment1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DA208E_Assignment1.Pages.Guests;

/// <summary>
/// The code-behind for the RSVP page of the website. Uses dependency injection to access guest service.
/// </summary>
public class RsvpModel : PageModel
{
    #region Fields
    private GuestService _guestService;
    #endregion

    #region Properties
    [BindProperty]
    public Guest Guest { get; set; }
    #endregion

    #region Constructors
    public RsvpModel(GuestService guestService)
    {
        _guestService = guestService;
        Guest = new Guest();
    }
    #endregion
    
    #region Methods
    /// <summary>
    /// Creates a new Guest from the submitted rsvp form data. Also passes TempData for a more dynamic confirmation page.
    /// </summary>
    /// <returns>The confirmation page after creation</returns>
    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) // Validation check, failed
            return Page();
        
        _guestService.Add(Guest); // Adding the new guest to the in-memory storage
        
        TempData["Attending"] = Guest.Attending; // Passing some guest data as TempData to make the confirmation page more dynamic
        
        return RedirectToPage("Confirmation"); // Redirecting to Confirmation page
    }
    
    /// <summary>
    /// Handles the HTTP GET request for this page. Not much being done here as of Assignment 1.
    /// </summary>
    public void OnGet()
    {
        
    }
    #endregion
}