using CargoWatch.Application.Models;
using MediatR;

namespace CargoWatch.Application.CQRS.GetStatisticsData;

public class GetStatisticsDataQuery : IRequest<List<StatisticsData>>
{
    public Guid DeviceId { get; }

    public GetStatisticsDataQuery(Guid deviceId)
    {
        DeviceId = deviceId;
    }
}