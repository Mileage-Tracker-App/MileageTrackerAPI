namespace MileageTrackerAPI.Models;

public class Log
{
    public int Id { get; set; }
    public string Vehicle { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public Dictionary<int, LogItem> Logs { get; set; } = new Dictionary<int, LogItem>();
    public int NumberOfLogs => Logs.Count;
    public double Miles => Logs.Count > 0 ? Logs[Logs.Count].Miles : 0;
}