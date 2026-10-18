// Sixten Peterson (AQ9300) 2026-10-18
namespace DA208E_Assignment2.Models;

/// <summary>
/// The DashboardViewModel consists of all the data needed for the dashboard view. Which includes guest, attendant,
/// event and useful information data.
/// </summary>
public class DashboardViewModel
{
    #region Properties
    public List<Guest> RegisteredGuests { get; set; }
    public List<Guest> AttendingGuests { get; set; }
    public List<Guest> NonAttendingGuests { get; set; }
    public int Attendants { get; set; }
    public List<Event> Events { get; set; }
    public List<UsefulInformation> UsefulInformation { get; set; }
    #endregion
}