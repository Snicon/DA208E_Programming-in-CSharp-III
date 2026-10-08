using DA208E_Assignment2.Data;
using Microsoft.AspNetCore.Mvc;

namespace DA208E_Assignment2.Controllers;

public class EventController : Controller
{
    #region Fields
    private readonly ApplicationDbContext _context;
    #endregion
    
    #region Constructors

    public EventController(ApplicationDbContext context)
    {
        _context = context;
    }
    #endregion
    
    public IActionResult Index()
    {
        
        return View();
    }
}