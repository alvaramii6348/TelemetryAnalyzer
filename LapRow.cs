using System;

namespace TelemetryAnalyzer;

public class LapRow
{
    public string? CarNumber {get; set;}
    public string? LastName {get; set;}
    public string? Sector {get; set;}
    public double Time {get; set;} 
    public double EntryTime {get; set;}
    public double ExitTime {get; set;}
    public int Lap {get; set;}
    public string? Flag {get; set;}
}
