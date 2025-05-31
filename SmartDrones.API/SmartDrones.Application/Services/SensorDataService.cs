using SmartDrones.Application.DTOs;
using SmartDrones.Application.Interfaces;
using SmartDrones.Domain.Entities;
using SmartDrones.Domain.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartDrones.Application.Services
{
    public class SensorDataService : ISensorDataService
    {
        private readonly ISensorDataRepository _sensorDataRepository;
        private readonly IDroneRepository _droneRepository;
        private readonly IMapper _mapper;

        public SensorDataService(ISensorDataRepository sensorDataRepository, IDroneRepository droneRepository, IMapper mapper)
        {
            _sensorDataRepository = sensorDataRepository;
            _droneRepository = droneRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SensorDataDto>> GetAllSensorDataAsync()
        {
            var sensorData = await _sensorDataRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SensorDataDto>>(sensorData);
        }

        public async Task<SensorDataDto?> GetSensorDataByIdAsync(Guid id)
        {
            var sensorData = await _sensorDataRepository.GetByIdAsync(id);
            return _mapper.Map<SensorDataDto>(sensorData);
        }

        public async Task<IEnumerable<SensorDataDto>> GetSensorDataByDroneIdAsync(Guid droneId)
        {
            var sensorData = await _sensorDataRepository.GetByDroneIdAsync(droneId);
            return _mapper.Map<IEnumerable<SensorDataDto>>(sensorData);
        }

        public async Task<SensorDataDto> AddSensorDataAsync(SensorDataDto sensorDataDto)
        {
            var droneExists = await _droneRepository.GetByIdAsync(sensorDataDto.DroneId) != null;
            if (!droneExists)
            {
                throw new ApplicationException($"Drone com ID {sensorDataDto.DroneId} não encontrado. Não é possível adicionar dados de sensor.");
            }

            var sensorData = new SensorData(
                sensorDataDto.DroneId,
                sensorDataDto.Temperature,
                sensorDataDto.Humidity,
                sensorDataDto.Luminosity,
                sensorDataDto.SmokeDetected,
                sensorDataDto.Latitude,
                sensorDataDto.Longitude
            );

            await _sensorDataRepository.AddAsync(sensorData);

            return _mapper.Map<SensorDataDto>(sensorData);
        }

        public async Task UpdateSensorDataAsync(SensorDataDto sensorDataDto)
        {
            if (sensorDataDto.Id == null || sensorDataDto.Id == Guid.Empty)
                throw new ArgumentException("ID do dado de sensor é obrigatório para atualização.");

            var existingSensorData = await _sensorDataRepository.GetByIdAsync(sensorDataDto.Id.Value);
            if (existingSensorData == null)
            {
                throw new ApplicationException($"Dado de sensor com ID {sensorDataDto.Id} não encontrado.");
            }

            existingSensorData.UpdateLocation(sensorDataDto.Latitude, sensorDataDto.Longitude);

            await _sensorDataRepository.UpdateAsync(existingSensorData);
        }

        public async Task DeleteSensorDataAsync(Guid id)
        {
            var sensorData = await _sensorDataRepository.GetByIdAsync(id);
            if (sensorData == null)
            {
                throw new ApplicationException($"Dado de sensor com ID {id} não encontrado.");
            }
            await _sensorDataRepository.DeleteAsync(sensorData);
        }
    }
}