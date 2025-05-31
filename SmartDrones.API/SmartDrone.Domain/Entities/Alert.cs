using System;
using SmartDrones.Domain.Enums;

namespace SmartDrones.Domain.Entities
{
    public class Alert
    {
        public Guid Id { get; private set; }
        public Guid DroneId { get; private set; }
        public DateTime Timestamp { get; private set; }
        public string Message { get; private set; }
        public RiskLevel RiskLevel { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public bool IsResolved { get; private set; }

        public Drone Drone { get; private set; } = null!;

        private Alert() { }

        public Alert(Guid droneId, string message, RiskLevel riskLevel, double latitude, double longitude)
        {
            if (droneId == Guid.Empty)
                throw new ArgumentException("DroneId cannot be empty.", nameof(droneId));
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Message cannot be null or empty.", nameof(message));

            Id = Guid.NewGuid();
            DroneId = droneId;
            Timestamp = DateTime.UtcNow;
            Message = message;
            RiskLevel = riskLevel;
            Latitude = latitude;
            Longitude = longitude;
            IsResolved = false;
        }

        public void ResolveAlert()
        {
            IsResolved = true;
        }

        public void UpdateMessage(string newMessage)
        {
            if (string.IsNullOrWhiteSpace(newMessage))
                throw new ArgumentException("Message cannot be null or empty.", nameof(newMessage));
            Message = newMessage;
        }

        public void UpdateRiskLevel(RiskLevel newRiskLevel)
        {
            RiskLevel = newRiskLevel;
        }
    }
}