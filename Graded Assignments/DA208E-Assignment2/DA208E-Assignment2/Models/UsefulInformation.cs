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
    public required string Title { get; set; }
    [Required]
    public required string Description { get; set; }
    #endregion
}