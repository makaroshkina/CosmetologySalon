using CosmetologySalon.Models;
using System.ComponentModel.DataAnnotations;

namespace CosmetologySalon.Models;

public class Appointment
{
    public int Id { get; set; }
    public DateTime AppointmentDateTime { get; set; }
    public AppointmentStatus Status { get; set; }

    
    // Foreign keys
    public int ClientId { get; set; }
    public int MasterId { get; set; }
    public int ServiceId { get; set; }

    // Navigation properties
    public virtual Client Client { get; set; }
    public virtual Master Master { get; set; } 
    public virtual Service Service { get; set; } 
}

public enum AppointmentStatus
{
     Scheduled = 1,      // ЗаписьСоздана
    Confirmed = 2,      // ЗаписьПодтверждена
    Rescheduled = 3,    // ЗаписьПеренесена
    Completed = 4,      // УслугаОказана
    Cancelled = 5       // ЗаписьОтменена       
}
