using CosmetologySalon.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using CosmetologySalon.Data;
using CosmetologySalon.DTOs;
using CosmetologySalon.Models;

namespace CosmetologySalon.Services
{
    public class MasterService : IMaster
    {
        private readonly SalonAppDbContext _context;

        public MasterService(SalonAppDbContext context)
        {
            _context = context;
        }

        public async Task<MasterDto?> GetMasterByNameAsync(string masterFullName)
        {
            if (string.IsNullOrWhiteSpace(masterFullName))
                return null;

            var master = await _context.Masters
                .FirstOrDefaultAsync(m => m.MasterFullName == masterFullName);

            if (master == null)
                return null;

            return await MapToMasterDto(master);
        }

        public async Task<List<MasterDto>> GetAllMastersAsync()
        {
            var masters = await _context.Masters.ToListAsync();
            var result = new List<MasterDto>();

            foreach (var master in masters)
            {
                result.Add(await MapToMasterDto(master));
            }

            return result;
        }

        public async Task<MasterDto?> GetMasterByPhoneAsync(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return null;

            var master = await _context.Masters
                .FirstOrDefaultAsync(m => m.MasterPhone == phone);

            if (master == null)
                return null;

            return await MapToMasterDto(master);
        }

        public async Task<MasterDto> AddMasterAsync(CreateMasterDto masterDto)
        {
            if (masterDto == null)
                throw new ArgumentNullException(nameof(masterDto));

            var master = new Master
            {
                MasterFullName = masterDto.MasterFullName,
                MasterPhone = masterDto.MasterPhone,
                MasterEmail = masterDto.MasterEmail,
                IsActive = true
            };

            await _context.Masters.AddAsync(master);
            await _context.SaveChangesAsync();

            return await MapToMasterDto(master);
        }

        public async Task<MasterDto?> UpdateMasterAsync(UpdateMasterDto masterDto)
        {
            if (masterDto == null)
                throw new ArgumentNullException(nameof(masterDto));

            var existingMaster = await _context.Masters
                .FirstOrDefaultAsync(m => m.MasterId == masterDto.MasterId);

            if (existingMaster == null)
                return null;

            existingMaster.MasterFullName = masterDto.MasterFullName;
            existingMaster.MasterPhone = masterDto.MasterPhone;
            existingMaster.MasterEmail = masterDto.MasterEmail;
            existingMaster.IsActive = masterDto.IsActive;

            _context.Masters.Update(existingMaster);
            await _context.SaveChangesAsync();

            return await MapToMasterDto(existingMaster);
        }

        public async Task<bool> RemoveMasterAsync(int masterId)
        {
            var master = await _context.Masters
                .FirstOrDefaultAsync(m => m.MasterId == masterId);

            if (master == null)
                return false;

            _context.Masters.Remove(master);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<MasterDto>> GetAllMastersWithCountIncludeAsync()
        {
            var masters = await _context.Masters
                .Select(m => new MasterDto
                {
                    MasterId = m.MasterId,
                    MasterFullName = m.MasterFullName,
                    MasterPhone = m.MasterPhone,
                    MasterEmail = m.MasterEmail,
                    IsActive = m.IsActive,
                    AppointmentsCount = m.Appointments.Count
                })
                .ToListAsync();

            return masters;
        }
                
        private async Task<MasterDto> MapToMasterDto(Master master)
        {
            var appointmentsCount = await _context.Appointments
                .CountAsync(a => a.MasterId == master.MasterId);

            return new MasterDto
            {
                MasterId = master.MasterId,
                MasterFullName = master.MasterFullName,
                MasterPhone = master.MasterPhone,
                MasterEmail = master.MasterEmail,
                IsActive = master.IsActive,
                AppointmentsCount = appointmentsCount
            };
        }
    }
}