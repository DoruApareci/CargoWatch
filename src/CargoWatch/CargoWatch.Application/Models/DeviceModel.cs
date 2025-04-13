namespace CargoWatch.Application.Models;

public class DeviceModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public uint MessageInterval { get; set; }
}
