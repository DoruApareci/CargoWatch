using CargoWatch.Application.Models;
using MediatR;

namespace CargoWatch.Application.CQRS.AddDataSet;

public class AddDataSetCommand : IRequest<Guid>
{
    public DataSetModel DataSet { get; set; }

    public AddDataSetCommand(DataSetModel dataSet)
    {
        DataSet = dataSet;
    }
}
