namespace CargoWatch.Domain.Entities;

public class DataSetEntity : BaseEntity
{
    public Guid SenderId { get; set; }
    public DeviceEntity Sender { get; set; }
    public DateTime TimeStamp { get; set; }
    public sbyte Temp { get; set; }
}
