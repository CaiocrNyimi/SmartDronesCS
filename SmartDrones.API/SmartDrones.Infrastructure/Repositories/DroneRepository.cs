using SmartDrones.Domain.Entities;
using SmartDrones.Domain.Interfaces;
using SmartDrones.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDrones.Infrastructure.Repositories
{
    public class DroneRepository : IDroneRepository
    {
        private readonly SmartDronesDbContext _context;

        public DroneRepository(SmartDronesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Drone>> GetAllAsync()
        {
            return await _context.Drones
                                 .Include(d => d.SensorData)
                                 .Include(d => d.Alerts)
                                 .ToListAsync();
        }

        public async Task<Drone?> GetByIdAsync(Guid id)
        {
            return await _context.Drones
                                 .Include(d => d.SensorData)
                                 .Include(d => d.Alerts)
                                 .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task AddAsync(Drone drone)
        {
            await _context.Drones.AddAsync(drone);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Drone drone)
        {
            _context.Drones.Update(drone);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Drone drone)
        {
            _context.Drones.Remove(drone);
            await _context.SaveChangesAsync();
        }
    }
}