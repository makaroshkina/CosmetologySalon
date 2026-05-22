using CosmetologySalon.Models;
namespace CosmetologySalon.DTOs;
public class ServiceDto
{
    public int ServiceId { get; set; }
    public string? ServiceName { get; set; }
    public int Price { get; set; }
    public int DurationMinutes { get; set; }
    public bool IsActive { get; set; }
}

public class CreateServiceDto
{
    public string ServiceName { get; set; }
    public int Price { get; set; }
    public int DurationMinutes { get; set; }
}

public class UpdateServiceDto
{
    public int ServiceId { get; set; }
    public string ServiceName { get; set; }
    public int Price { get; set; }
    public int DurationMinutes { get; set; }
    public bool IsActive { get; set; }
}
