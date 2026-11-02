namespace DA208E_Assignment3.ViewModels;

public class GuestViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string? Email { get; set; }
    public bool? Attending { get; set; }
    public int? NumberOfAttendants  { get; set; }
}