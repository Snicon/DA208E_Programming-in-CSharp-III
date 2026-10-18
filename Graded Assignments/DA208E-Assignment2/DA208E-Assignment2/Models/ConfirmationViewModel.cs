// Sixten Peterson (AQ9300) 2026-10-18
namespace DA208E_Assignment2.Models;

/// <summary>
/// ViewModel used for the RSVP confirmation found in the GuestController (confirmation action)
/// Consists of a Guest and could be simplified but is here for any eventual future changes
/// </summary>
public class ConfirmationViewModel
{
    #region Properties
    public Guest Guest { get; set; }
    #endregion
}