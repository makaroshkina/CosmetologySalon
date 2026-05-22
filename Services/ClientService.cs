using CosmetologySalon.Models;
using CosmetologySalon.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using CosmetologySalon.Data;
using CosmetologySalon.DTOs;

namespace CosmetologySalon.Services
{
    public class ClientService : IClient
    {
        private readonly SalonAppDbContext _context;

        public ClientService(SalonAppDbContext context)
        {
            _context = context;
        }

        public async Task<ClientDto?> GetClientByPhoneAsync(string clientPhone)
        {
            if (string.IsNullOrWhiteSpace(clientPhone))
                return null;

            var client = await _context.Clients
                .FirstOrDefaultAsync(c => c.ClientPhone == clientPhone);

            if (client == null)
                return null;

            return await MapToClientDto(client);
        }

        public async Task<ClientDto?> GetClientByNameAsync(string clientFullName)
        {
            if (string.IsNullOrWhiteSpace(clientFullName))
                return null;

            var client = await _context.Clients
                .FirstOrDefaultAsync(c => c.ClientFullName == clientFullName);

            if (client == null)
                return null;

            return await MapToClientDto(client);
        }

        public async Task<List<ClientDto>> GetAllClientsAsync()
        {
            var clients = await _context.Clients.ToListAsync();
            var result = new List<ClientDto>();

            foreach (var client in clients)
            {
                result.Add(await MapToClientDto(client));
            }

            return result;
        }

        public async Task<ClientDto> RegisterClientAsync(CreateClientDto clientDto)
        {
            if (clientDto == null)
                throw new ArgumentNullException(nameof(clientDto));

            var client = new Client
            {
                ClientFullName = clientDto.ClientFullName,
                ClientPhone = clientDto.ClientPhone,
                ClientEmail = clientDto.ClientEmail,
                IsActive = true
            };

            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();

            return await MapToClientDto(client);
        }

        public async Task<ClientDto?> UpdateClientAsync(UpdateClientDto clientDto)
        {
            if (clientDto == null)
                throw new ArgumentNullException(nameof(clientDto));

            var existingClient = await _context.Clients
                .FirstOrDefaultAsync(c => c.ClientId == clientDto.ClientId);

            if (existingClient == null)
                return null;

            existingClient.ClientFullName = clientDto.ClientFullName;
            existingClient.ClientPhone = clientDto.ClientPhone;
            existingClient.ClientEmail = clientDto.ClientEmail;
            existingClient.IsActive = clientDto.IsActive;

            _context.Clients.Update(existingClient);
            await _context.SaveChangesAsync();

            return await MapToClientDto(existingClient);
        }

        public async Task<bool> UpdateClientContactsAsync(int clientId, string? clientEmail, string clientPhone)
        {
            var client = await _context.Clients
                .FirstOrDefaultAsync(c => c.ClientId == clientId);

            if (client == null)
                return false;

            if (!string.IsNullOrWhiteSpace(clientPhone))
                client.ClientPhone = clientPhone;

            if (clientEmail != null)
                client.ClientEmail = clientEmail;

            _context.Clients.Update(client);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveClientAsync(int clientId)
        {
            var client = await _context.Clients
                .FirstOrDefaultAsync(c => c.ClientId == clientId);

            if (client == null)
                return false;

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeactivateClientAsync(int clientId)
        {
            var client = await _context.Clients
                .FirstOrDefaultAsync(c => c.ClientId == clientId);

            if (client == null)
                return false;

            client.IsActive = false;
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ActivateClientAsync(int clientId)
        {
            var client = await _context.Clients
                .FirstOrDefaultAsync(c => c.ClientId == clientId);

            if (client == null)
                return false;

            client.IsActive = true;
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<int> GetClientAppointmentsCountAsync(int clientId)
        {
            return await _context.Appointments
                .CountAsync(a => a.ClientId == clientId);
        }

        private async Task<ClientDto> MapToClientDto(Client client)
        {
            var appointmentsCount = await _context.Appointments
                .CountAsync(a => a.ClientId == client.ClientId);

            return new ClientDto
            {
                ClientId = client.ClientId,
                ClientFullName = client.ClientFullName,
                ClientPhone = client.ClientPhone,
                ClientEmail = client.ClientEmail,
                IsActive = client.IsActive,
                AppointmentsCount = appointmentsCount
            };
        }
    }
}