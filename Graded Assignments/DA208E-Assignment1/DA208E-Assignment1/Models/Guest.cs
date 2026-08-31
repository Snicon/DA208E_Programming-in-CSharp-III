using System.ComponentModel.DataAnnotations;

namespace DA208E_Assignment1.Models;

public class Guest
{
    #region Properties
    public int Id { get; set; }
    
    [StringLength(60, MinimumLength = 3)]
    [Required]
    public required string Name { get; set; }
    
    [Required]
    [EmailAddress] // From p.164 in ASP.NET Core in action (Third edition) by Andrew Lock
    public required string Email { get; set; }
    
    [Required]
    public required bool Attending { get; set; }
    
    [StringLength(200)] // Maximum of 200 chars according to p.5 "Assignment 1 - Help" document
    public required string Message { get; set; }
    #endregion
}