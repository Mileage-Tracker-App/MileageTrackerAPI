using MileageTrackerAPI.Models;
using MileageTrackerAPI;
using Microsoft.EntityFrameworkCore;

namespace MileageTrackerAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Log> Logs { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<LogItem> LogItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Log>()
            .HasOne<Session>()
            .WithMany(s => s.Logs)
            .HasForeignKey(l => l.SessionId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<LogItem>()
            .HasOne<Log>()
            .WithMany(l => l.LogItems)
            .HasForeignKey(li => li.LogId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}