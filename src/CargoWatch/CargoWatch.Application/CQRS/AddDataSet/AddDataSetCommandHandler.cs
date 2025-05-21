using AutoMapper;
using CargoWatch.Application.Interfaces;
using CargoWatch.Application.Models;
using CargoWatch.Domain.Entities;
using MediatR;

namespace CargoWatch.Application.CQRS.AddDataSet;

public class AddDataSetCommandHandler : IRequestHandler<AddDataSetCommand, Guid>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IEmailService _email;

    public AddDataSetCommandHandler(IAppDbContext context, IMapper mapper, IEmailService email)
    {
        _context = context;
        _mapper = mapper;
        _email = email;
    }

    public async Task<Guid> Handle(AddDataSetCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<DataSetEntity>(request.DataSet);
        entity.TimeStamp = DateTime.UtcNow;

        _context.Messages.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        var deviceEntity = _context.Devices.First(x => x.Id == entity.SenderId);
        var device = _mapper.Map<DeviceModel>(deviceEntity);

        var minTemp = device.NeededTemperature - device.Diapazon;
        var maxTemp = device.NeededTemperature + device.Diapazon;

        if (entity.Temp < minTemp || entity.Temp> maxTemp)
        {
            var subject = $"Alert: Temperature out of range for device {device.Name}";
            var body = $@"
                            <html>
                            <head>
                              <style>
                                body {{ font-family: Arial, sans-serif; line-height: 1.5; color: #333; }}
                                .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
                                h2 {{ color: #d9534f; }}
                                p {{ margin: 8px 0; }}
                                .label {{ font-weight: bold; }}
                              </style>
                            </head>
                            <body>
                              <div class=""container"">
                                <h2>Alert for device: {device.Name}</h2>
                                <p><span class=""label"">Received temperature:</span> {entity.Temp}°</p>
                                <p><span class=""label"">Allowed range:</span> [{minTemp}°, {maxTemp}°]</p>
                                <p><span class=""label"">Message received at (UTC):</span> {entity.TimeStamp:yyyy-MM-dd HH:mm:ss}</p>
                              </div>
                            </body>
                            </html>";


            var emailRequest = _email.FormEmailRequest(
                emailFrom: null,
                emailTo: device.ContactEmail,
                subject: subject,
                body: body);

            await _email.SendEmail(emailRequest);
        }

        return entity.Id;
    }
}
