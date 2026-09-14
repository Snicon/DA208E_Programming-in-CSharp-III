// Sixten Peterson (AQ9300) 2026-09-14

using DA208E_Assignment1.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DA208E_Assignment1.Pages;

/// <summary>
/// The code-behind for the index page of the website. Uses dependency injection to access event data.
/// </summary>
public class IndexModel : PageModel
{
    #region Properties
    public Event EventInfo { get; set; }
    #endregion

    #region Constructors
    public IndexModel(Event eventInfo)
    {
        EventInfo = eventInfo;
    }
    #endregion
    
    #region Methods
    /// <summary>
    /// Handles the HTTP GET request for this page. Not much being done here as of Assignment 1.
    /// </summary>
    public void OnGet()
    {
        
    }
    #endregion
}