// Sixten Peterson (AQ9300) 2026-10-16
using System.ComponentModel.DataAnnotations;

namespace DA208E_Assignment2.Models;

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