using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DA208E_Assignment1.Pages;

public class Event : PageModel
{
    public Models.Event EventInfo { get; set; }
    
    public void OnGet()
    {
        // TODO: Move and use Dependency Injection for reuse...
        EventInfo = new Models.Event()
        {
            Title = "Wedding",
            Date = DateOnly.FromDateTime(DateTime.Now),
            Time = new TimeOnly().AddHours(15),
            Location = "Springfield Wedding Hall, Lund, Sweden"
        };
    }
}