// Sixten Peterson (AQ9300) 2026-10-08
namespace DA208E_Assignment2.Models;

/// <summary>
/// TODO: Update comment
/// </summary>
public class Event
{
    #region Properties
    public int Id { get; set; }
    public required string Title { get; set; }
    public required DateTime DateTime { get; set; }
    public required string Description { get; set; }
    #endregion
}