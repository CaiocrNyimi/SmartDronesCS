using SmartDrones.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartDrones.Application.Interfaces
{
    public interface IAlertService
    {
        Task<IEnumerable<AlertDto>> GetAllAlertsAsync();
        Task<AlertDto?> GetAlertByIdAsync(Guid id);
        Task<AlertDto> CreateAlertAsync(AlertDto alertDto);
        Task UpdateAlertAsync(AlertDto alertDto);
        Task DeleteAlertAsync(Guid id);
        Task ResolveAlertAsync(Guid alertId);
    }
}