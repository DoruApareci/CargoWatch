namespace CargoWatch.Application.Models;

public class DeviceModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public uint MessageInterval { get; set; }
    public string ContactEmail { get; set; }
    public int Diapazon { get; set; }
    public int NeededTemperature { get; set; }
    public string SSID { get; set; }
    public string SSID_Password { get; set; }
}
