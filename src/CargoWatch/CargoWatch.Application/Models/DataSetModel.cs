namespace CargoWatch.Application.Models;

public class DataSetModel
{
    public Guid Id { get; set; }
    public Guid SenderId { get; set; }
    public DeviceModel? Sender { get; set; }
    public DateTime TimeStamp { get; set; }
    public sbyte Temp { get; set; }
}
