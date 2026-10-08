using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DA208E_Assignment2.Models;

namespace DA208E_Assignment2.Controllers;

public class HomeController : Controller
{
    #region Fields
    //
    #endregion
    
    #region Constructors

    public HomeController()
    {
        //
    }
    #endregion
    
    public IActionResult Index()
    {
        return View();
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