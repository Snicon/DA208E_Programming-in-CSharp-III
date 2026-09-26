using DA208E_Assignment2.Data;
using DA208E_Assignment2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DA208E_Assignment2.Controllers;

public class GuestController : Controller
{
    private readonly ApplicationDbContext _context;
    
    public GuestController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IActionResult> Index()
    {
        var guests = await _context.Guests.ToListAsync();
        
        return View(guests);
    }
    
    // GET: /Guest/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    // POST: /Guest/Create
    [HttpPost]
    public async Task<IActionResult> Create(Guest guest)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        
        _context.Guests.Add(guest);
        await _context.SaveChangesAsync();
        
        return RedirectToAction(nameof(Index));
    }
}