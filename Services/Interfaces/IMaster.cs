using CosmetologySalon.DTOs;
using CosmetologySalon.Models;

namespace CosmetologySalon.Services.Interfaces
{
    public interface IMaster
    {
        Task<MasterDto?> GetMasterByNameAsync(string masterFullName);
        Task<List<MasterDto>> GetAllMastersAsync();
        Task<MasterDto?> GetMasterByPhoneAsync(string phone);
        Task<MasterDto> AddMasterAsync(CreateMasterDto masterDto);
        Task<MasterDto?> UpdateMasterAsync(UpdateMasterDto masterDto);
        Task<bool> RemoveMasterAsync(int masterId);
        Task<List<MasterDto>> GetAllMastersWithCountIncludeAsync();
    }
}