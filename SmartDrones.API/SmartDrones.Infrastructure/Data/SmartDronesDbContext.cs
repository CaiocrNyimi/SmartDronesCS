using Microsoft.EntityFrameworkCore;
using SmartDrones.Domain.Entities;

namespace SmartDrones.Infrastructure.Data
{
    public class SmartDronesDbContext : DbContext
    {
        public SmartDronesDbContext(DbContextOptions<SmartDronesDbContext> options) : base(options) { }

        public DbSet<Drone> Drones { get; set; }
        public DbSet<SensorData> SensorData { get; set; }
        public DbSet<Alert> Alerts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Drone>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Id).ValueGeneratedOnAdd();
                entity.Property(d => d.Identifier).IsRequired().HasMaxLength(50);
                entity.Property(d => d.Model).IsRequired().HasMaxLength(100);
                entity.Property(d => d.Status).IsRequired().HasMaxLength(50);
                entity.Property(d => d.LastActivity).IsRequired();

                entity.HasMany(d => d.SensorData)
                      .WithOne(sd => sd.Drone)
                      .HasForeignKey(sd => sd.DroneId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(d => d.Alerts)
                      .WithOne(a => a.Drone)
                      .HasForeignKey(a => a.DroneId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<SensorData>(entity =>
            {
                entity.HasKey(sd => sd.Id);
                entity.Property(sd => sd.Id).ValueGeneratedOnAdd();
                entity.Property(sd => sd.Timestamp).IsRequired();
                entity.Property(sd => sd.Temperature).IsRequired();
                entity.Property(sd => sd.Humidity).IsRequired();
                entity.Property(sd => sd.Luminosity).IsRequired();
                entity.Property(sd => sd.SmokeDetected).IsRequired();
                entity.Property(sd => sd.Latitude).IsRequired();
                entity.Property(sd => sd.Longitude).IsRequired();
            });

            modelBuilder.Entity<Alert>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Id).ValueGeneratedOnAdd();
                entity.Property(a => a.Timestamp).IsRequired();
                entity.Property(a => a.Message).IsRequired().HasMaxLength(500);
                entity.Property(a => a.RiskLevel).IsRequired();
                entity.Property(a => a.Latitude).IsRequired();
                entity.Property(a => a.Longitude).IsRequired();
                entity.Property(a => a.IsResolved).IsRequired();
            });
        }
    }
}