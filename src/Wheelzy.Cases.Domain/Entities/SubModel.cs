namespace Wheelzy.Cases.Domain.Entities;

public class SubModel
{
    public int SubModelId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int ModelId { get; set; }
}