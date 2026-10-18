// Sixten Peterson (AQ9300) 2026-10-18
using DA208E_Assignment2.Models;
using Microsoft.EntityFrameworkCore;

namespace DA208E_Assignment2.Data;

/// <summary>
/// The database context of the application, more or less exactly copied and adapted from the lecture/reading material.
/// </summary>
public class ApplicationDbContext : DbContext
{
    #region Constructor
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
    #endregion

    #region Properties / DbSets
    public DbSet<Guest> Guests { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<UsefulInformation> UsefulInformation { get; set; }
    #endregion
}