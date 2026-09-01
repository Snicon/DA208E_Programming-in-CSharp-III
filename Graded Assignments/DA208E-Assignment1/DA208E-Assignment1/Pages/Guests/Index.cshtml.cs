using DA208E_Assignment1.Data;
using DA208E_Assignment1.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DA208E_Assignment1.Pages.Guests;

public class Index : PageModel
{
    private GuestService _guestService;
    
    public List<Guest> Guests { get; set; }

    public Index(GuestService guestService)
    {
        _guestService = guestService;
    }
    
    public void OnGet()
    {
        Guests = _guestService.GetAll();
    }
}