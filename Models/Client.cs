using System.ComponentModel.DataAnnotations;

namespace CosmetologySalon.Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        [Required]
        [MaxLength(200)]
        public string ClientFullName { get; set; }
        [Required]
        [Phone]
        [MaxLength(11)]
        public string ClientPhone { get; set; }
        [EmailAddress]
        public string? ClientEmail { get; set; }
        public bool IsActive { get; set; } = true;
        // Navigation property
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
