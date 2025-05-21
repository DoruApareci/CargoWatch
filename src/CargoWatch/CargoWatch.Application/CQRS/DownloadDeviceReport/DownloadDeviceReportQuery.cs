using MediatR;

namespace CargoWatch.Application.CQRS.DownloadDeviceReport;

public record DownloadDeviceReportQuery(Guid DeviceId, DateTime StartDate, DateTime EndDate) : IRequest<string>;
