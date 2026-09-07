using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DA208E_Assignment1.Pages;

public class Event : PageModel
{
    public Models.Event EventInfo { get; set; }

    public Event(Models.Event eventInfo)
    {
        EventInfo = eventInfo;
    }
    
    public void OnGet()
    {

    }
}