namespace CargoWatch.Domain.Entities;

public class DeviceEntity : BaseEntity
{
    public string Name { get; set; }
    public uint MessageInterval { get; set; }
    public ICollection<DataSetEntity> Messages { get; set; }
}
