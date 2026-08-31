namespace DA208E_Assignment1.Models;

public class Event
{
    #region Properties
    public required string Title { get; set; }
    public required DateOnly Date { get; set; }
    public required TimeOnly Time { get; set; }
    public required string Location { get; set; }
    #endregion
}