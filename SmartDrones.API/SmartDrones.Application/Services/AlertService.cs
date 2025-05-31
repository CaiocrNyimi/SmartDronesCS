using SmartDrones.Application.DTOs;
using SmartDrones.Application.Interfaces;
using SmartDrones.Domain.Entities;
using SmartDrones.Domain.Enums;
using SmartDrones.Domain.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartDrones.Application.Services
{
    public class AlertService : IAlertService
    {
        private readonly IAlertRepository _alertRepository;
        private readonly IDroneRepository _droneRepository;
        private readonly IMapper _mapper;

        public AlertService(IAlertRepository alertRepository, IDroneRepository droneRepository, IMapper mapper)
        {
            _alertRepository = alertRepository;
            _droneRepository = droneRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AlertDto>> GetAllAlertsAsync()
        {
            var alerts = await _alertRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AlertDto>>(alerts);
        }

        public async Task<AlertDto?> GetAlertByIdAsync(Guid id)
        {
            var alert = await _alertRepository.GetByIdAsync(id);
            return _mapper.Map<AlertDto>(alert);
        }

        public async Task<AlertDto> CreateAlertAsync(AlertDto alertDto)
        {
            var droneExists = await _droneRepository.GetByIdAsync(alertDto.DroneId) != null;
            if (!droneExists)
            {
                throw new ApplicationException($"Drone com ID {alertDto.DroneId} não encontrado. Não é possível criar alerta.");
            }

            var alert = new Alert(
                alertDto.DroneId,
                alertDto.Message,
                alertDto.RiskLevel,
                alertDto.Latitude,
                alertDto.Longitude
            );

            await _alertRepository.AddAsync(alert);
            return _mapper.Map<AlertDto>(alert);
        }

        public async Task UpdateAlertAsync(AlertDto alertDto)
        {
            if (alertDto.Id == null || alertDto.Id == Guid.Empty)
                throw new ArgumentException("ID do alerta é obrigatório para atualização.");

            var existingAlert = await _alertRepository.GetByIdAsync(alertDto.Id.Value);
            if (existingAlert == null)
            {
                throw new ApplicationException($"Alerta com ID {alertDto.Id} não encontrado.");
            }

            existingAlert.UpdateMessage(alertDto.Message);
            existingAlert.UpdateRiskLevel(alertDto.RiskLevel);
            if (existingAlert.IsResolved != alertDto.IsResolved)
            {
                if (alertDto.IsResolved)
                {
                    existingAlert.ResolveAlert();
                }
            }

            await _alertRepository.UpdateAsync(existingAlert);
        }

        public async Task DeleteAlertAsync(Guid id)
        {
            var alert = await _alertRepository.GetByIdAsync(id);
            if (alert == null)
            {
                throw new ApplicationException($"Alerta com ID {id} não encontrado.");
            }
            await _alertRepository.DeleteAsync(alert);
        }

        public async Task ResolveAlertAsync(Guid alertId)
        {
            var alert = await _alertRepository.GetByIdAsync(alertId);
            if (alert == null)
            {
                throw new ApplicationException($"Alerta com ID {alertId} não encontrado.");
            }

            if (!alert.IsResolved)
            {
                alert.ResolveAlert();
                await _alertRepository.UpdateAsync(alert);
            }
        }
    }
}