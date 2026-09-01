using DA208E_Assignment1.Data;
using DA208E_Assignment1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DA208E_Assignment1.Pages.Guests;

public class Delete : PageModel
{
    private GuestService _guestService;
    
    [BindProperty]
    public Guest? Guest { get; set; }

    public Delete(GuestService guestService)
    {
        _guestService = guestService;
    }
    
    public IActionResult OnGet(int id)
    {
        Guest = _guestService.Get(id);
        if (Guest == null)
        {
            return RedirectToPage("Index");
        }
        
        return Page();
    }

    public IActionResult OnPost()
    {
        if (Guest != null)
            _guestService.Delete(Guest.Id);
        
        return RedirectToPage("Index");
    }
}