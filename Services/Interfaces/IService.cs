using CosmetologySalon.DTOs;

namespace CosmetologySalon.Services.Interfaces
{
    public interface IService
    {
        // Получение данных
        Task<ServiceDto?> GetServiceByNameAsync(string serviceName);
        Task<IEnumerable<ServiceDto>> GetAllServicesAsync();
        Task<IEnumerable<ServiceDto>> GetAvailableServicesAsync();

        // Управление услугами
        Task<ServiceDto> AddServiceAsync(CreateServiceDto serviceDto);
        Task<ServiceDto> UpdateServiceAsync(UpdateServiceDto serviceDto);
        Task<bool> RemoveServiceAsync(int serviceId);
        Task<bool> ActivateServiceAsync(int serviceId);
        Task<bool> DeactivateServiceAsync(int serviceId);
    }
}