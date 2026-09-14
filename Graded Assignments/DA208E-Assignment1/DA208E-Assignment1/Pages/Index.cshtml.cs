using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DA208E_Assignment1.Pages;

public class IndexModel : PageModel
{
    public Models.Event EventInfo { get; set; }

    public IndexModel(Models.Event eventInfo)
    {
        EventInfo = eventInfo;
    }
    
    public void OnGet()
    {
        
    }
}