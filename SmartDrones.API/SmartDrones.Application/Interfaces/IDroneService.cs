using SmartDrones.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartDrones.Application.Interfaces
{
    public interface IDroneService
    {
        Task<IEnumerable<DroneDto>> GetAllDronesAsync();
        Task<DroneDto?> GetDroneByIdAsync(Guid id);
        Task<DroneDto> CreateDroneAsync(DroneDto droneDto);
        Task UpdateDroneAsync(DroneDto droneDto);
        Task DeleteDroneAsync(Guid id);
    }
}