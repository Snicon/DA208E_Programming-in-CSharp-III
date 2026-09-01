using DA208E_Assignment1.Data;
using DA208E_Assignment1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DA208E_Assignment1.Pages.Guests;

public class Edit : PageModel
{
    private GuestService _guestService;
    
    [BindProperty]
    public Guest Guest { get; set; }

    public Edit(GuestService guestService)
    {
        _guestService = guestService;
    }
    
    public IActionResult OnGet(int id)
    {
        Guest = _guestService.Get(id);

        if (Guest == null)
            return RedirectToPage("Index");
        
        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) // Validation check, failed
            return Page();
        
        _guestService.Update(Guest);
        return RedirectToPage("Index");
    }
}