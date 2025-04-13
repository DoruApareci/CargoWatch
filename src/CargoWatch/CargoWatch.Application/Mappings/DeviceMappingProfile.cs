using AutoMapper;
using CargoWatch.Application.Models;
using CargoWatch.Domain.Entities;

namespace CargoWatch.Domain.Mappings;

public  class DeviceMappingProfile : Profile
{
    public DeviceMappingProfile()
    {
        CreateMap<DeviceModel, DeviceEntity>();
        CreateMap<DeviceEntity, DeviceModel>();
    }
}
