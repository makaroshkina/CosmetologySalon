using CosmetologySalon.Models;
namespace CosmetologySalon.DTOs;

public class ClientDto
{
    public int ClientId { get; set; }
    public string ClientFullName { get; set; } = string.Empty;
    public string ClientPhone { get; set; } = string.Empty;
    public string? ClientEmail { get; set; }
    public bool IsActive { get; set; }
    public int AppointmentsCount { get; set; }
}

public class CreateClientDto
{
    public string ClientFullName { get; set; } = string.Empty;
    public string ClientPhone { get; set; } = string.Empty;
    public string? ClientEmail { get; set; }
}

public class UpdateClientDto
{
    public int ClientId { get; set; }
    public string ClientFullName { get; set; } = string.Empty;
    public string ClientPhone { get; set; } = string.Empty;
    public string? ClientEmail { get; set; }
    public bool IsActive { get; set; }
}