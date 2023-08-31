using System.Collections.Generic;

namespace VibeOne.Models;

public class TankModel
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public double Temperature { get; init; }
    public IEnumerable<TemperatureHistory>? TemperatureHistory { get; init; }
}

public class TemperatureHistory
{
    public double Temperature { get; set; }
    public DateTime Time { get; set; }
}
