using CargoWatch.Application.Interfaces;
using CargoWatch.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CargoWatch.Application.CQRS.GetStatisticsData;

public class GetStatisticsDataQueryHandler : IRequestHandler<GetStatisticsDataQuery, List<StatisticsData>>
{
    private readonly IAppDbContext _context;

    public GetStatisticsDataQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<StatisticsData>> Handle(GetStatisticsDataQuery request, CancellationToken cancellationToken)
    {
        return await _context.Messages
            .Where(m => m.SenderId == request.DeviceId)
            .OrderBy(m => m.TimeStamp)
            .Select(m => new StatisticsData
            {
                Temp = m.Temp,
                TimeStamp = m.TimeStamp
            })
            .ToListAsync(cancellationToken);
    }
}
