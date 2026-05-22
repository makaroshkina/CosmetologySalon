using CosmetologySalon.Models;
using CosmetologySalon.DTOs;

namespace CosmetologySalon.Services.Interfaces
{
    public interface IClient
    {
        Task<ClientDto?> GetClientByPhoneAsync(string clientPhone);
        Task<ClientDto?> GetClientByNameAsync(string clientFullName);
        Task<List<ClientDto>> GetAllClientsAsync();
        Task<ClientDto> RegisterClientAsync(CreateClientDto clientDto);
        Task<ClientDto?> UpdateClientAsync(UpdateClientDto clientDto);
        Task<bool> UpdateClientContactsAsync(int clientId, string? clientEmail, string clientPhone);
        Task<bool> RemoveClientAsync(int clientId);
        Task<bool> DeactivateClientAsync(int clientId);
        Task<bool> ActivateClientAsync(int clientId);
        Task<int> GetClientAppointmentsCountAsync(int clientId);
    }
}