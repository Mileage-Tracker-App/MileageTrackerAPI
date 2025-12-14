using Microsoft.EntityFrameworkCore;
using MileageTrackerAPI.Models;

namespace MileageTrackerAPI.Data
{
    public class MileageTrackerContext : DbContext
    {
        public MileageTrackerContext(DbContextOptions<MileageTrackerContext> options) : base(options) {}

        public DbSet<Vehicle> Vehicles { get; set; }
    }
}