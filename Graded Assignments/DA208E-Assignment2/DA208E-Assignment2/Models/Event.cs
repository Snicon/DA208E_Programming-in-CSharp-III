// Sixten Peterson (AQ9300) 2026-10-08
using System.ComponentModel.DataAnnotations;

namespace DA208E_Assignment2.Models;

/// <summary>
/// The event model represents an event taking place within the wedding event. This data is presented in the event
/// schedule.
/// </summary>
public class Event
{
    #region Properties
    public int Id { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public required string Title { get; set; }
    [Required]
    public required DateTime DateTime { get; set; }
    [Required]
    [StringLength(150)] // Maximum of 150 chars according to p.5 "Assignment 1 - Help" document
    public required string Description { get; set; }
    #endregion
}