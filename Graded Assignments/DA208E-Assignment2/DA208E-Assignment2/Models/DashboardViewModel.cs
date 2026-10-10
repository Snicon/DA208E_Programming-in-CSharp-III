namespace DA208E_Assignment2.Models;

public class DashboardViewModel
{
    public int RegisteredGuests { get; set; }
    public int AttendingGuests { get; set; }
    public int NonAttendingGuests { get; set; }
    public int Attendants { get; set; }
    public List<Event> Events { get; set; }
}