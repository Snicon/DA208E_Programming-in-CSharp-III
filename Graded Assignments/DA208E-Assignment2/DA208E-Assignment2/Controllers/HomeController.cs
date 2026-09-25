using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DA208E_Assignment2.Models;

namespace DA208E_Assignment2.Controllers;

public class HomeController : Controller
{
    #region Fields
    private readonly Event _eventInfo;
    #endregion
    
    #region Constructors

    public HomeController(Event eventInfo)
    {
        _eventInfo = eventInfo;
    }
    #endregion
    
    public IActionResult Index()
    {
        return View(_eventInfo);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}