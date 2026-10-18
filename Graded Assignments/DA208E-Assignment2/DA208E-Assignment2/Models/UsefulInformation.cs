// Sixten Peterson (AQ9300) 2026-10-16
using System.ComponentModel.DataAnnotations;

namespace DA208E_Assignment2.Models;

/// <summary>
/// Represents a piece of information about the wedding in general that may be useful for the guests to know.
/// An icon is included along the title and description to make it more attractive in the website design.
/// </summary>
public class UsefulInformation
{
    #region Properties
    public int Id { get; set; }
    [Required]
    public required string Icon { get; set; }
    [Required]
    [StringLength(30, MinimumLength = 3)]
    public required string Title { get; set; }
    [Required]
    [StringLength(150, MinimumLength = 20)]
    public required string Description { get; set; }
    #endregion
}