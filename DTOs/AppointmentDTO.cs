using CosmetologySalon.Models;
namespace CosmetologySalon.DTOs;
public class AppointmentDto
{
    public int Id { get; set; }
    public DateTime AppointmentDateTime { get; set; }
    public AppointmentStatus Status { get; set; }
    public int ClientId { get; set; }
    public int MasterId { get; set; }
    public int ServiceId { get; set; }
}

public class CreateAppointmentDto
{
    public DateTime AppointmentDateTime { get; set; }
    public int ClientId { get; set; }
    public int MasterId { get; set; }
    public int ServiceId { get; set; }
}

public class UpdateAppointmentDto
{
    public int Id { get; set; }
    public DateTime AppointmentDateTime { get; set; }
    public AppointmentStatus Status { get; set; }
    public int ClientId { get; set; }
    public int MasterId { get; set; }
    public int ServiceId { get; set; }
}