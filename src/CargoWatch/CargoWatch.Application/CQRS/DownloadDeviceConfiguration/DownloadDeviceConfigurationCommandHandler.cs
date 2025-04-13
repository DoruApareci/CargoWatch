using AutoMapper;
using CargoWatch.Application.Interfaces;
using CargoWatch.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace CargoWatch.Application.CQRS.DownloadDeviceConfiguration;

public class DownloadDeviceConfigurationCommandHandler : IRequestHandler<DownloadDeviceConfigurationCommand, string>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public DownloadDeviceConfigurationCommandHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<string> Handle(DownloadDeviceConfigurationCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Devices
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.DeviceId, cancellationToken);

        if (entity == null)
            throw new InvalidOperationException("Device not found");

        var model = _mapper.Map<DeviceModel>(entity);
        var json = JsonSerializer.Serialize(model, new JsonSerializerOptions { WriteIndented = true });

        return json;
    }
}
