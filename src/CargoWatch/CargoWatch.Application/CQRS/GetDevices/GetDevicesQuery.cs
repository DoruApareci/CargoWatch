using CargoWatch.Application.Models;
using MediatR;

namespace CargoWatch.Application.CQRS.GetDevices;

public class GetDevicesQuery : IRequest<List<DeviceModel>> { }
