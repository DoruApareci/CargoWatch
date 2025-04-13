using AutoMapper;
using CargoWatch.Application.Interfaces;
using CargoWatch.Domain.Entities;
using MediatR;

namespace CargoWatch.Application.CQRS.AddDataSet;

public class AddDataSetCommandHandler : IRequestHandler<AddDataSetCommand, Guid>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public AddDataSetCommandHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(AddDataSetCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<DataSetEntity>(request.DataSet);
        entity.TimeStamp = DateTime.UtcNow;

        _context.Messages.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
