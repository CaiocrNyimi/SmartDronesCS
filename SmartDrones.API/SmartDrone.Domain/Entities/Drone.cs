using System;
using System.Collections.Generic;

namespace SmartDrones.Domain.Entities
{
    public class Drone
    {
        public Guid Id { get; private set; }
        public string Identifier { get; private set; }
        public string Model { get; private set; }
        public string Status { get; private set; }
        public DateTime LastActivity { get; private set; }

        public ICollection<SensorData> SensorData { get; private set; }
        public ICollection<Alert> Alerts { get; private set; }

        private Drone()
        {
            SensorData = new List<SensorData>();
            Alerts = new List<Alert>();
        }

        public Drone(string identifier, string model)
        {
            if (string.IsNullOrWhiteSpace(identifier))
                throw new ArgumentException("Identifier cannot be null or empty.", nameof(identifier));
            if (string.IsNullOrWhiteSpace(model))
                throw new ArgumentException("Model cannot be null or empty.", nameof(model));

            Id = Guid.NewGuid();
            Identifier = identifier;
            Model = model;
            Status = "Offline";
            LastActivity = DateTime.UtcNow;

            SensorData = new List<SensorData>();
            Alerts = new List<Alert>();
        }
        public void UpdateStatus(string newStatus)
        {
            if (string.IsNullOrWhiteSpace(newStatus))
                throw new ArgumentException("Status cannot be null or empty.", nameof(newStatus));

            Status = newStatus;
            LastActivity = DateTime.UtcNow;
        }

        public void UpdateDetails(string newIdentifier, string newModel)
        {
            if (string.IsNullOrWhiteSpace(newIdentifier))
                throw new ArgumentException("Identifier cannot be null or empty.", nameof(newIdentifier));
            if (string.IsNullOrWhiteSpace(newModel))
                throw new ArgumentException("Model cannot be null or empty.", nameof(newModel));

            Identifier = newIdentifier;
            Model = newModel;
            LastActivity = DateTime.UtcNow;
        }
    }
}