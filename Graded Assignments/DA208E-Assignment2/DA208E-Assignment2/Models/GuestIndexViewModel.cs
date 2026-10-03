namespace DA208E_Assignment2.Models;

public class GuestIndexViewModel
{
    public List<Guest> Guests { get; set; }
    public List<Guest> AttendingGuests { get; set; }
    public List<Guest> NotAttendingGuests { get; set; }
}