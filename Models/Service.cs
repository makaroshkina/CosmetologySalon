using System.ComponentModel.DataAnnotations;

namespace CosmetologySalon.Models;
public class Service
{
    [Key]
    public int ServiceId { get; set; }
    [Required]
    [MaxLength(200)]
    public string? ServiceName { get; set; }
    [Required]
    public int Price { get; set; }
    [Required]
    public int DurationMinutes { get; set; }
    public bool IsActive { get; set; } = true;

    // Navigation property
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}