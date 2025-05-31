using SmartDrones.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartDrones.Application.Interfaces
{
    public interface ISensorDataService
    {
        Task<IEnumerable<SensorDataDto>> GetAllSensorDataAsync();
        Task<SensorDataDto?> GetSensorDataByIdAsync(Guid id);
        Task<IEnumerable<SensorDataDto>> GetSensorDataByDroneIdAsync(Guid droneId);
        Task<SensorDataDto> AddSensorDataAsync(SensorDataDto sensorDataDto);
        Task UpdateSensorDataAsync(SensorDataDto sensorDataDto);
        Task DeleteSensorDataAsync(Guid id);
    }
}