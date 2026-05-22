using CosmetologySalon.Data;
using CosmetologySalon.Models;
using CosmetologySalon.Services;
using CosmetologySalon.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Настройка DbContext
builder.Services.AddDbContext<SalonAppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Регистрация сервисов
builder.Services.AddScoped<IAppointment, AppointmentService>();
builder.Services.AddScoped<IClient, ClientService>();
builder.Services.AddScoped<IMaster, MasterService>();
builder.Services.AddScoped<IService, ServiceService>();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SalonAppDbContext>();
    try
    {
        Console.WriteLine("=== ПЕРЕЗАГРУЗКА БАЗЫ ДАННЫХ ===");

        // Удаляем старую базу
        await dbContext.Database.EnsureDeletedAsync();
        Console.WriteLine("База данных удалена");

        // Создаем новую базу
        await dbContext.Database.EnsureCreatedAsync();
        Console.WriteLine("База данных создана");

        // Добавляем тестовые данные вручную
        Console.WriteLine("Добавление тестовых данных...");

        // Клиенты
        dbContext.Clients.AddRange(
            new Client { ClientFullName = "Анна Иванова", ClientPhone = "9123456789", ClientEmail = "anna@example.com", IsActive = true },
            new Client { ClientFullName = "Мария Петрова", ClientPhone = "9234567890", ClientEmail = "maria@example.com", IsActive = true },
            new Client { ClientFullName = "Екатерина Сидорова", ClientPhone = "9345678901", ClientEmail = "ekaterina@example.com", IsActive = true },
            new Client { ClientFullName = "Ольга Кузнецова", ClientPhone = "9456789012", ClientEmail = "olga@example.com", IsActive = true },
            new Client { ClientFullName = "Татьяна Соколова", ClientPhone = "9567890123", ClientEmail = "tatiana@example.com", IsActive = true },
            new Client { ClientFullName = "Наталья Попова", ClientPhone = "9678901234", ClientEmail = "natalia@example.com", IsActive = false }
        );

        // Мастера
        dbContext.Masters.AddRange(
            new Master { MasterFullName = "Елена Прекрасная", MasterPhone = "9012345678", MasterEmail = "elena@salon.com", IsActive = true },
            new Master { MasterFullName = "Дмитрий Власов", MasterPhone = "9123456780", MasterEmail = "dmitry@salon.com", IsActive = true },
            new Master { MasterFullName = "Светлана Морозова", MasterPhone = "9234567891", MasterEmail = "svetlana@salon.com", IsActive = true },
            new Master { MasterFullName = "Александр Невский", MasterPhone = "9345678902", MasterEmail = "alex@salon.com", IsActive = true },
            new Master { MasterFullName = "Ирина Волкова", MasterPhone = "9456789013", MasterEmail = "irina@salon.com", IsActive = false }
        );

        // Услуги
        dbContext.Services.AddRange(
            new Service { ServiceName = "Чистка лица", Price = 2500, DurationMinutes = 60, IsActive = true }, 
            new Service { ServiceName = "Массаж", Price = 3000, DurationMinutes = 90, IsActive = true }, 
            new Service { ServiceName = "Маникюр", Price = 1500, DurationMinutes = 45, IsActive = true }, 
            new Service { ServiceName = "Педикюр", Price = 2000, DurationMinutes = 60, IsActive = true }, 
            new Service { ServiceName = "Уход за волосами", Price = 3500, DurationMinutes = 120, IsActive = true }, 
            new Service { ServiceName = "Пилинг лица", Price = 2800, DurationMinutes = 50, IsActive = true }, 
            new Service { ServiceName = "Ботокс для волос", Price = 4000, DurationMinutes = 90, IsActive = false }
        );

        await dbContext.SaveChangesAsync();

        Console.WriteLine("Тестовые данные добавлены");
        Console.WriteLine("База данных готова к работе");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ошибка: {ex.Message}");
        if (ex.InnerException != null)
        {
            Console.WriteLine($"Внутренняя ошибка: {ex.InnerException.Message}");
        }
    }
}

// Настройка конвейера HTTP-запросов
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();