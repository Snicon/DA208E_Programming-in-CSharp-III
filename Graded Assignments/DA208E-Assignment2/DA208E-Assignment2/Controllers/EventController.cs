using DA208E_Assignment2.Models;
using Microsoft.AspNetCore.Mvc;

namespace DA208E_Assignment2.Controllers;

public class EventController : Controller
{
    #region Fields
    private readonly Event _eventInfo;
    #endregion
    
    #region Constructors

    public EventController(Event eventInfo)
    {
        _eventInfo = eventInfo;
    }
    #endregion
    
    public IActionResult Index()
    {
        return View(_eventInfo);
    }
}