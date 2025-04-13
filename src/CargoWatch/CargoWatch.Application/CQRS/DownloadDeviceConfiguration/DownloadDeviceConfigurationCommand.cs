using MediatR;

namespace CargoWatch.Application.CQRS.DownloadDeviceConfiguration;

public class DownloadDeviceConfigurationCommand : IRequest<string>
{
    public Guid DeviceId { get; }

    public DownloadDeviceConfigurationCommand(Guid deviceId)
    {
        DeviceId = deviceId;
    }
}
