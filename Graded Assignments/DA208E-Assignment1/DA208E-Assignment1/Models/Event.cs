// Sixten Peterson (AQ9300) 2026-09-14
namespace DA208E_Assignment1.Models;

/// <summary>
/// Event Model representing an event. No data annotations are used for validation since so far I only create one instance myself in Program.cs and don't have to worry about user input.
/// </summary>
public class Event
{
    #region Properties
    public required string Title { get; set; }
    public required DateOnly Date { get; set; }
    public required TimeOnly Time { get; set; }
    public required DateOnly RsvpByDate { get; set; }
    public required string Location { get; set; }
    #endregion
}