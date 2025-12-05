namespace MileageTrackerAPI.Models;

public class Session
{
    public int Id { get; set; }
    public Dictionary<int, Log> Logs { get; set; } = new Dictionary<int, Log>();
}