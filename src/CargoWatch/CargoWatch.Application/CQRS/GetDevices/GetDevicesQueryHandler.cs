using AutoMapper;
using CargoWatch.Application.Interfaces;
using CargoWatch.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CargoWatch.Application.CQRS.GetDevices;

internal class GetDevicesQueryHandler : IRequestHandler<GetDevicesQuery, List<DeviceModel>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetDevicesQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DeviceModel>> Handle(GetDevicesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.Devices.AsNoTracking().ToListAsync(cancellationToken);
        return _mapper.Map<List<DeviceModel>>(entities);
    }
}
