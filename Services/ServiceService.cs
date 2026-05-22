using CosmetologySalon.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using CosmetologySalon.Data;
using CosmetologySalon.Models;
using CosmetologySalon.DTOs;

namespace CosmetologySalon.Services
{
    public class ServiceService : IService
    {
        private readonly SalonAppDbContext _context;

        public ServiceService(SalonAppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceDto?> GetServiceByNameAsync(string serviceName)
        {
            var service = await _context.Services
                .FirstOrDefaultAsync(s => s.ServiceName == serviceName);

            if (service == null)
                return null;

            return MapToServiceDto(service);
        }

        public async Task<IEnumerable<ServiceDto>> GetAllServicesAsync()
        {
            var services = await _context.Services
                .OrderBy(s => s.ServiceName)
                .ToListAsync();

            return services.Select(MapToServiceDto);
        }

        public async Task<IEnumerable<ServiceDto>> GetAvailableServicesAsync()
        {
            var services = await _context.Services
                .Where(s => s.IsActive)
                .OrderBy(s => s.ServiceName)
                .ToListAsync();

            return services.Select(MapToServiceDto);
        }

        public async Task<ServiceDto> AddServiceAsync(CreateServiceDto serviceDto)
        {
            if (serviceDto == null)
                throw new ArgumentNullException(nameof(serviceDto));

            var service = new Service
            {
                ServiceName = serviceDto.ServiceName,
                Price = serviceDto.Price,
                DurationMinutes = serviceDto.DurationMinutes,
                IsActive = true
            };

            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            return MapToServiceDto(service);
        }

        public async Task<ServiceDto> UpdateServiceAsync(UpdateServiceDto serviceDto)
        {
            if (serviceDto == null)
                throw new ArgumentNullException(nameof(serviceDto));

            var existingService = await _context.Services.FindAsync(serviceDto.ServiceId);
            if (existingService == null)
                throw new ArgumentException("Услуга не найдена");

            existingService.ServiceName = serviceDto.ServiceName;
            existingService.Price = serviceDto.Price;
            existingService.DurationMinutes = serviceDto.DurationMinutes;
            existingService.IsActive = serviceDto.IsActive;

            await _context.SaveChangesAsync();
            return MapToServiceDto(existingService);
        }

        public async Task<bool> RemoveServiceAsync(int serviceId)
        {
            var service = await _context.Services.FindAsync(serviceId);
            if (service == null)
                return false;

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActivateServiceAsync(int serviceId)
        {
            var service = await _context.Services.FindAsync(serviceId);
            if (service == null)
                return false;

            service.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeactivateServiceAsync(int serviceId)
        {
            var service = await _context.Services.FindAsync(serviceId);
            if (service == null)
                return false;

            service.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }
               
        private static ServiceDto MapToServiceDto(Service service)
        {
            return new ServiceDto
            {
                ServiceId = service.ServiceId,
                ServiceName = service.ServiceName,
                Price = service.Price,
                DurationMinutes = service.DurationMinutes,
                IsActive = service.IsActive
            };
        }
    }
}