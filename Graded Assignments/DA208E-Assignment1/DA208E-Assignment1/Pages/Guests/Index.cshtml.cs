// Sixten Peterson (AQ9300) 2026-09-14
using DA208E_Assignment1.Data;
using DA208E_Assignment1.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DA208E_Assignment1.Pages.Guests;

/// <summary>
/// The code-behind for the Index page for managing guests. Uses dependency injection to access guest service.
/// </summary>
public class IndexModel : PageModel
{
    #region Fields
    private GuestService _guestService;
    #endregion

    #region Properties
    public List<Guest> Guests { get; set; } // List of all guests
    #endregion

    #region Constructors
    public IndexModel(GuestService guestService)
    {
        _guestService = guestService;
    }
    #endregion
    
    /// <summary>
    /// Handles the HTTP GET request for this page. Sets Guests to list of all guests from guest service.
    /// </summary>
    #region Methods
    public void OnGet()
    {
        Guests = _guestService.GetAll();
    }
    #endregion
}