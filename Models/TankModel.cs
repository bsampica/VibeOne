namespace VibeOne.Models;

public class TankModel
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public double Temperature { get; init; }
    public bool SomethingHappening { get; init; }

    public override string ToString()
    {
        return $"{Name}:{Id}";
    }
}