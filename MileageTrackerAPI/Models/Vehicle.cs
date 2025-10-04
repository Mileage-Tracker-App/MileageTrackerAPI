namespace MileageTrackerAPI.Models;

public class Vehicle
{
    public int Id { get; set; } // Primary key
    public string Name { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public string Year { get; set; }
}