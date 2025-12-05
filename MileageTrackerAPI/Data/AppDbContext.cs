using MileageTrackerAPI.Models;
using MileageTrackerAPI;
using Microsoft.EntityFrameworkCore;

namespace MileageTrackerAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Log> Logs { get; set; }
    public DbSet<Session> Sessions { get; set; }
}