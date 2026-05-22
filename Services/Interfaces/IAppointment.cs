using CosmetologySalon.Models;
using CosmetologySalon.DTOs;

namespace CosmetologySalon.Services.Interfaces
{
    public interface IAppointment
    {
        
        Task<AppointmentDto?> GetAppointmentByIdAsync(int appointmentId);// Получить запись по ID

        Task<List<AppointmentDto>> GetAppointmentsByClientIdAsync(int clientId);// Получить все записи клиента с фильтрацией по статусу

        Task<List<AppointmentDto>> GetAppointmentsByClientIdWithStatusAsync(int clientId, AppointmentStatus? status = null);// Создать новую запись

        Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto appointmentDto);// Подтвердить запись

        Task<bool> ConfirmAppointmentAsync(int appointmentId);// Перенести запись

        Task<AppointmentDto?> RescheduleAppointmentAsync(int appointmentId, DateTime newDateTime);// Завершить услугу (изменить статус на УслугаОказана)

        Task<bool> CompleteAppointmentAsync(int appointmentId);

        Task<List<AppointmentDto>> GetAllAppointmentsAsync();
    }
}