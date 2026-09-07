using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DA208E_Assignment1.Pages.Guests;

public class Confirmation : PageModel
{
    public Models.Event EventInfo { get; set; }

    public Confirmation(Models.Event eventInfo)
    {
        EventInfo = eventInfo;
    }
    
    public void OnGet()
    {
        
    }
}