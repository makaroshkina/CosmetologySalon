using CosmetologySalon.Data;
using CosmetologySalon.Models;
using CosmetologySalon.Services.Interfaces;
using CosmetologySalon.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CosmetologySalon.Services
{
    public class AppointmentService : IAppointment
    {
        private readonly SalonAppDbContext _context;

        public AppointmentService(SalonAppDbContext context)
        {
            _context = context;
        }

        public async Task<AppointmentDto?> GetAppointmentByIdAsync(int appointmentId)
        {
            var appointment = await _context.Appointments
                .Include(a => a.Client)
                .Include(a => a.Master)
                .Include(a => a.Service)
                .FirstOrDefaultAsync(a => a.Id == appointmentId);

            if (appointment == null)
                return null;

            return MapToAppointmentDto(appointment);
        }

        public async Task<List<AppointmentDto>> GetAppointmentsByClientIdAsync(int clientId)
        {
            var appointments = await _context.Appointments
                .Where(a => a.ClientId == clientId)
                .Include(a => a.Client)
                .Include(a => a.Master)
                .Include(a => a.Service)
                .ToListAsync();

            return appointments.Select(MapToAppointmentDto).ToList();
        }

        public async Task<List<AppointmentDto>> GetAppointmentsByClientIdWithStatusAsync(int clientId, AppointmentStatus? status = null)
        {
            IQueryable<Appointment> query = _context.Appointments
                .Where(a => a.ClientId == clientId)
                .Include(a => a.Client)
                .Include(a => a.Master)
                .Include(a => a.Service);

            if (status.HasValue)
            {
                query = query.Where(a => a.Status == status.Value);
            }

            var appointments = await query.ToListAsync();
            return appointments.Select(MapToAppointmentDto).ToList();
        }

        public async Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto appointmentDto)
        {
            if (appointmentDto == null)
                throw new ArgumentNullException(nameof(appointmentDto));

            // Проверка существования клиента
            var client = await _context.Clients.FindAsync(appointmentDto.ClientId);
            if (client == null)
                throw new ArgumentException($"Клиент с ID {appointmentDto.ClientId} не найден");

            // Проверка существования мастера
            var master = await _context.Masters.FindAsync(appointmentDto.MasterId);
            if (master == null)
                throw new ArgumentException($"Мастер с ID {appointmentDto.MasterId} не найден");

            // Проверка существования услуги
            var service = await _context.Services.FindAsync(appointmentDto.ServiceId);
            if (service == null)
                throw new ArgumentException($"Услуга с ID {appointmentDto.ServiceId} не найдена");

            // Проверка на пересечение времени
            var hasConflict = await _context.Appointments
                .AnyAsync(a => a.MasterId == appointmentDto.MasterId &&
                               a.AppointmentDateTime == appointmentDto.AppointmentDateTime &&
                               a.Status != AppointmentStatus.Cancelled);

            if (hasConflict)
                throw new InvalidOperationException("У мастера уже есть запись на это время");

            var appointment = new Appointment
            {
                ClientId = appointmentDto.ClientId,
                MasterId = appointmentDto.MasterId,
                ServiceId = appointmentDto.ServiceId,
                AppointmentDateTime = appointmentDto.AppointmentDateTime,
                Status = AppointmentStatus.Scheduled,
            };

            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();

            // Загружаем навигационные свойства для DTO
            await _context.Entry(appointment)
                .Reference(a => a.Client)
                .LoadAsync();
            await _context.Entry(appointment)
                .Reference(a => a.Master)
                .LoadAsync();
            await _context.Entry(appointment)
                .Reference(a => a.Service)
                .LoadAsync();

            return MapToAppointmentDto(appointment);
        }

        public async Task<bool> ConfirmAppointmentAsync(int appointmentId)
        {
            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(a => a.Id == appointmentId);

            if (appointment == null)
                return false;

            appointment.Status = AppointmentStatus.Confirmed;
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<AppointmentDto?> RescheduleAppointmentAsync(int appointmentId, DateTime newDateTime)
        {
            var appointment = await _context.Appointments
                .Include(a => a.Client)
                .Include(a => a.Master)
                .Include(a => a.Service)
                .FirstOrDefaultAsync(a => a.Id == appointmentId);

            if (appointment == null)
                return null;

            // Проверка на пересечение времени с новым временем
            var hasConflict = await _context.Appointments
                .AnyAsync(a => a.MasterId == appointment.MasterId &&
                               a.AppointmentDateTime == newDateTime &&
                               a.Id != appointmentId &&
                               a.Status != AppointmentStatus.Cancelled);

            if (hasConflict)
                throw new InvalidOperationException("У мастера уже есть запись на это время");

            appointment.AppointmentDateTime = newDateTime;
            appointment.Status = AppointmentStatus.Rescheduled;

            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();

            return MapToAppointmentDto(appointment);
        }

        public async Task<bool> CompleteAppointmentAsync(int appointmentId)
        {
            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(a => a.Id == appointmentId);

            if (appointment == null)
                return false;

            appointment.Status = AppointmentStatus.Completed;
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<AppointmentDto>> GetAllAppointmentsAsync()
        {
            var appointments = await _context.Appointments
                .Include(a => a.Client)
                .Include(a => a.Master)
                .Include(a => a.Service)
                .ToListAsync();

            return appointments.Select(MapToAppointmentDto).ToList();
        }

        private static AppointmentDto MapToAppointmentDto(Appointment appointment)
        {
            return new AppointmentDto
            {
                Id = appointment.Id,
                AppointmentDateTime = appointment.AppointmentDateTime,
                Status = appointment.Status,
                ClientId = appointment.ClientId,
                MasterId = appointment.MasterId,
                ServiceId = appointment.ServiceId
            };
        }
    }
}