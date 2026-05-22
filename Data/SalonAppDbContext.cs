using Microsoft.EntityFrameworkCore;
using CosmetologySalon.Models;

namespace CosmetologySalon.Data
{
    public class SalonAppDbContext : DbContext
    {
        public SalonAppDbContext(DbContextOptions<SalonAppDbContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Master> Masters { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          

            // Appointment configuration
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.AppointmentDateTime).IsRequired();
                    
                entity.Property(e => e.Status).IsRequired();


                // Relationships
                entity.HasOne(a => a.Client)
                    .WithMany(c => c.Appointments)
                    .HasForeignKey(a => a.ClientId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(a => a.Master)
                    .WithMany(m => m.Appointments)
                    .HasForeignKey(a => a.MasterId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.Service)
                    .WithMany(s => s.Appointments)
                    .HasForeignKey(a => a.ServiceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Client configuration
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.ClientId);

                entity.Property(e => e.ClientFullName).IsRequired();
                    
                entity.Property(e => e.ClientPhone).IsRequired();
                    
                entity.Property(e => e.ClientEmail);

                // Indexes
                entity.HasIndex(e => e.ClientPhone).IsUnique();
                    
                entity.HasIndex(e => e.ClientEmail).IsUnique();
            });

            // Master configuration
            modelBuilder.Entity<Master>(entity =>
            {
                entity.HasKey(e => e.MasterId);

                entity.Property(e => e.MasterFullName).IsRequired();
                    
                entity.Property(e => e.MasterPhone).IsRequired();
                    

                // Indexes
                entity.HasIndex(e => e.MasterPhone).IsUnique();
                    
                entity.HasIndex(e => e.MasterEmail).IsUnique();
            });

            // Service configuration
            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(e => e.ServiceId);

                entity.Property(e => e.ServiceName).IsRequired();
                
                entity.Property(e => e.Price).IsRequired();
                    
                entity.Property(e => e.DurationMinutes).IsRequired();
                    
            });
        }
    }
}