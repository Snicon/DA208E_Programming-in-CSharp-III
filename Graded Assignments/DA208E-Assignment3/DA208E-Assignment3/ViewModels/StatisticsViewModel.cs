namespace DA208E_Assignment3.ViewModels;

public class StatisticsViewModel
{
    public int TotalRegisteredGuests  { get; set; }
    public int GuestResponses { get; set; }
    public double RsvpResponseRate { get; set; }
    public int AttendingGuests { get; set; }
    public double GuestAttendRate { get; set; }
    public int NonAttendingGuests { get; set; }
    public double GuestNonAttendRate { get; set; }
    public int AttendingAttendants { get; set; }
    public int AttendantsAccompanyingRegisteredGuests { get; set; }
}