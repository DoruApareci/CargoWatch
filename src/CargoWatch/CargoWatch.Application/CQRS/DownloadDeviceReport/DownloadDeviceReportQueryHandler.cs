using CargoWatch.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace CargoWatch.Application.CQRS.DownloadDeviceReport;

internal class DownloadDeviceReportQueryHandler(IAppDbContext context) : IRequestHandler<DownloadDeviceReportQuery, string>
{
    public async Task<string> Handle(DownloadDeviceReportQuery request, CancellationToken cancellationToken)
    {
        var device = await context.Devices
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == request.DeviceId, cancellationToken);

        if (device == null)
            throw new InvalidOperationException("Dispozitivul nu a fost găsit.");

        var messages = await context.Messages
            .AsNoTracking()
            .Where(m => m.SenderId == request.DeviceId &&
                        m.TimeStamp >= request.StartDate &&
                        m.TimeStamp <= request.EndDate)
            .OrderBy(m => m.TimeStamp)
            .ToListAsync(cancellationToken);

        var sb = new StringBuilder();
        sb.AppendLine($"Raport Mesaje Dispozitiv: {device.Name} (ID: {device.Id})");
        sb.AppendLine($"Perioada: {request.StartDate:yyyy-MM-dd} - {request.EndDate:yyyy-MM-dd}");
        sb.AppendLine("------------------------------------------------------------");

        if (messages.Count == 0)
        {
            sb.AppendLine("Nu s-au găsit mesaje în intervalul specificat.");
        }
        else
        {
            foreach (var msg in messages)
            {
                sb.AppendLine($"{msg.TimeStamp:yyyy-MM-dd HH:mm:ss} - Temperatura: {msg.Temp}°C");
            }
        }

        return sb.ToString();
    }
}
