// Sixten Peterson (AQ9300) 2026-11-02
namespace DA208E_Assignment3.ViewModels;

// TODO: Comment
public class GuestDetailsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public bool? Attending { get; set; }
    public int? NumberOfAttendants  { get; set; }
    public string? Message { get; set; }
}