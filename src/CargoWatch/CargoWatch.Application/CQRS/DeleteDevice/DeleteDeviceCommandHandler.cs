using CargoWatch.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CargoWatch.Application.CQRS.DeleteDevice;

public class DeleteDeviceCommandHandler : IRequestHandler<DeleteDeviceCommand, Unit>
{
    private readonly IAppDbContext _context;

    public DeleteDeviceCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteDeviceCommand request, CancellationToken cancellationToken)
    {
        var device = await _context.Devices
            .Include(d => d.Messages)
            .FirstOrDefaultAsync(d => d.Id == request.DeviceId, cancellationToken);

        if (device == null)
            throw new InvalidOperationException("Device not found.");

        foreach(var m in device.Messages)
        {
            m.IsDeleted = true;
            m.DeletedOn = DateTime.Now;
        }

        device.IsDeleted = true;
        device.DeletedOn = DateTime.Now;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
