using CosmetologySalon.Models;
namespace CosmetologySalon.DTOs;

public class MasterDto
{
    public int MasterId { get; set; }
    public string? MasterFullName { get; set; }
    public string MasterPhone { get; set; }
    public string? MasterEmail { get; set; }
    public bool IsActive { get; set; }
    public int? AppointmentsCount { get; set; }

}

public class CreateMasterDto
{
    public string MasterFullName { get; set; }
    public string MasterPhone { get; set; }
    public string? MasterEmail { get; set; }
}

public class UpdateMasterDto
{
    public int MasterId { get; set; }
    public string MasterFullName { get; set; }
    public string MasterPhone { get; set; }
    public string? MasterEmail { get; set; }
    public bool IsActive { get; set; }
    public int? AppointmentsCount { get; set; }
}
