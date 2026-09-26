using DA208E_Assignment2.Models;
using Microsoft.EntityFrameworkCore;

namespace DA208E_Assignment2.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
    
    public DbSet<Guest> Guests { get; set; }
}