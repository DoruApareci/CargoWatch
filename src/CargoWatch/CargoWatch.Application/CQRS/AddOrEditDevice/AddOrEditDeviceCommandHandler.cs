using AutoMapper;
using CargoWatch.Application.Interfaces;
using CargoWatch.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CargoWatch.Application.CQRS.AddOfEditDevice;

public class AddOrEditDeviceCommandHandler : IRequestHandler<AddOrEditDeviceCommand, Guid>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;
    public AddOrEditDeviceCommandHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(AddOrEditDeviceCommand request, CancellationToken cancellationToken)
    {
        var model = request.Device;

        var entity = await _context.Devices
            .FirstOrDefaultAsync(d => d.Id == model.Id, cancellationToken);

        if (entity == null)
        {
            entity = _mapper.Map<DeviceEntity>(model);
            _context.Devices.Add(entity);
        }
        else
            _mapper.Map(model, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
