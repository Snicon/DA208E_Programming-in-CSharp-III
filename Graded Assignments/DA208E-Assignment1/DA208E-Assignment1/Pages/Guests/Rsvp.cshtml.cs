using DA208E_Assignment1.Data;
using DA208E_Assignment1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DA208E_Assignment1.Pages.Guests;

public class Rsvp : PageModel
{
    private GuestService _guestService;
    
    [BindProperty]
    public Guest Guest { get; set; }

    public Rsvp(GuestService guestService)
    {
        _guestService = guestService;
        Guest = new Guest();
    }
    
    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) // Validation check, failed
            return Page();
        
        _guestService.Add(Guest);
        
        return RedirectToPage("Confirmation");
    }
    
    public void OnGet()
    {
        
    }
}