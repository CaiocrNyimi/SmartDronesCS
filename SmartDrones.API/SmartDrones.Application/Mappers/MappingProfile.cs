using AutoMapper;
using SmartDrones.Application.DTOs;
using SmartDrones.Domain.Entities;

namespace SmartDrones.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Drone, DroneDto>().ReverseMap();

            CreateMap<SensorData, SensorDataDto>().ReverseMap();

            CreateMap<Alert, AlertDto>().ReverseMap();
        }
    }
}