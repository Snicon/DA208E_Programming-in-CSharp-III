namespace DA208E_Assignment2.Models;

public class DashboardViewModel
{
    public List<Guest> RegisteredGuests { get; set; }
    public List<Guest> AttendingGuests { get; set; }
    public List<Guest> NonAttendingGuests { get; set; }
    public int Attendants { get; set; }
    public List<Event> Events { get; set; }
}