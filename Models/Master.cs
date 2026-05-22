using System.ComponentModel.DataAnnotations;
using CosmetologySalon.Models;

namespace CosmetologySalon.Models;
public class Master
{
    [Key]
    public int MasterId { get; set; }
    [Required]
    [MaxLength(200)]
    public string? MasterFullName { get; set; }
    [Required]
    [Phone]
    [MaxLength(11)]
    public string MasterPhone { get; set; }
    [EmailAddress]
    public string? MasterEmail { get; set; }
    public bool IsActive { get; set; } = true;

    // Navigation property
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}