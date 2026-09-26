using DA208E_Assignment2.Models;
using Microsoft.AspNetCore.Mvc;

namespace DA208E_Assignment2.Controllers;

public class GuestController : Controller
{
    public IActionResult Index()
    {
        var guests = new List<Guest>
        {
            new Guest { Id = 1, Name = "John", Email = "john@doe.com" },
            new Guest { Id = 2, Name = "Jane", Email = "jane@doe.com" }
        };
        
        return View(guests);
    }
    
    public IActionResult Create()
    {
        return View();
    }
}