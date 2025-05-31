using System;

namespace SmartDrones.Domain.Entities
{
    public class SensorData
    {
        public Guid Id { get; private set; }
        public Guid DroneId { get; private set; }
        public DateTime Timestamp { get; private set; }
        public double Temperature { get; private set; }
        public double Humidity { get; private set; }
        public double Luminosity { get; private set; }
        public bool SmokeDetected { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public Drone Drone { get; private set; } = null!;

        private SensorData() { }

        public SensorData(Guid droneId, double temperature, double humidity, double luminosity, bool smokeDetected, double latitude, double longitude)
        {
            if (droneId == Guid.Empty)
                throw new ArgumentException("DroneId cannot be empty.", nameof(droneId));

            Id = Guid.NewGuid();
            DroneId = droneId;
            Timestamp = DateTime.UtcNow;
            Temperature = temperature;
            Humidity = humidity;
            Luminosity = luminosity;
            SmokeDetected = smokeDetected;
            Latitude = latitude;
            Longitude = longitude;
        }

        public void UpdateLocation(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}