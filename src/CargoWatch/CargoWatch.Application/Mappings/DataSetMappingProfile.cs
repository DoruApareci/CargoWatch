using AutoMapper;
using CargoWatch.Application.Models;
using CargoWatch.Domain.Entities;

namespace CargoWatch.Domain.Mappings;

public class DataSetMappingProfile : Profile
{
    public DataSetMappingProfile()
    {
        CreateMap<DataSetModel, DataSetEntity>();
        CreateMap<DataSetEntity, DataSetModel>();
    }
}
