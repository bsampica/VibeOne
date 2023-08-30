using System.Collections.Generic;

namespace VibeOne.Models;

public class TankModel
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public double Temperature { get; init; }
    public IEnumerable<double>? TemperatureHistory { get; init; }

    public TankModel()
    {
        
    }
}
