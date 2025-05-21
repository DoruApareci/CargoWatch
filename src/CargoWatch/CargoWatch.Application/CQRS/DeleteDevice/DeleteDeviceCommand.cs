using MediatR;

namespace CargoWatch.Application.CQRS.DeleteDevice;

public record DeleteDeviceCommand(Guid DeviceId) : IRequest<Unit>;
