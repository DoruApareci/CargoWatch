using CargoWatch.Application.Models;
using MediatR;

namespace CargoWatch.Application.CQRS.AddOfEditDevice;

public class AddOrEditDeviceCommand : IRequest<Guid>
{
    public DeviceModel Device { get; set; }

    public AddOrEditDeviceCommand(DeviceModel model)
    {
        Device = model;
    }
}
