// Sixten Peterson (AQ9300) 2026-09-14
using DA208E_Assignment1.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DA208E_Assignment1.Pages.Guests;

/// <summary>
/// The code-behind for the Confirmation page of the website. Uses dependency injection to access event data.
/// </summary>
public class ConfirmationModel : PageModel
{
    #region Properties
    public Event EventInfo { get; set; }
    #endregion

    #region Constructors
    public ConfirmationModel(Event eventInfo)
    {
        EventInfo = eventInfo;
    }
    #endregion

    /// <summary>
    /// Handles the HTTP GET request for this page. Not much being done here as of Assignment 1.
    /// </summary>
    #region Methods
    public void OnGet()
    {
        
    }
    #endregion
}