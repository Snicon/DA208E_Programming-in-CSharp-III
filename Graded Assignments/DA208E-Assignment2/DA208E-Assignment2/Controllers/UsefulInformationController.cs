using DA208E_Assignment2.Data;
using DA208E_Assignment2.Models;
using Microsoft.AspNetCore.Mvc;

namespace DA208E_Assignment2.Controllers;

public class UsefulInformationController : Controller
{
    #region Fields
    private readonly ApplicationDbContext _context;
    #endregion
    
    public UsefulInformationController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(UsefulInformation usefulInformation)
    {
        if (!ModelState.IsValid)
            return View();

        _context.UsefulInformation.Add(usefulInformation);
        await _context.SaveChangesAsync();
        
        return RedirectToAction("Index", "Dashboard");
    }
}