using DA208E_Assignment3.Data;
using DA208E_Assignment3.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DA208E_Assignment3.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StatisticsApiController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public StatisticsApiController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<StatisticsViewModel>> GetStatistics()
    {
        var totalGuests = await _context.Guests.CountAsync();
        var guestResponses = await _context.Guests.CountAsync(g => g.Attending != null);
        var attendingGuests = await _context.Guests.Where(g => g.Attending == true).CountAsync();
        var nonAttendingGuests = await _context.Guests.Where(g => g.Attending == false).CountAsync();
            
        var model = new StatisticsViewModel
        {
            TotalRegisteredGuests = totalGuests,
            GuestResponses = guestResponses,
            RsvpResponseRate = totalGuests == 0
                ? 0
                : (double)guestResponses / totalGuests,
            
            AttendingGuests = attendingGuests,
            GuestAttendRate = totalGuests == 0
                ? 0
                : (double)attendingGuests / totalGuests,
            
            NonAttendingGuests = nonAttendingGuests,
            GuestNonAttendRate = totalGuests == 0
                ? 0
                : (double)nonAttendingGuests / totalGuests,
            AttendingAttendants = await _context.Guests.Where(g => g.Attending == true)
                .SumAsync(g => g.NumberOfAttendants ?? 0),
            AttendantsAccompanyingRegisteredGuests = await _context.Guests.Where(g => g.Attending == true)
                .SumAsync(g => g.NumberOfAttendants - 1 ?? 0)
        };

        return Ok(model);
    }
}