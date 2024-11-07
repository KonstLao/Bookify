using Bookify.Domain.Bookings;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Статический класс для регистрации зависимостей прикладного уровня
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Метод расширения для регистрации зависимостей прикладного уровня
    /// </summary>
    /// <param name="services"> Коллекция сервисов </param>
    /// <returns></returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Регистрация сервисов библиотеки MediatR
        services.AddMediatR(configuration =>
        {
            // Регистрация обработчиков команд и запросов из текущей сборки (базовый функционал)
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });
        //Регистрация доменных сервисов
        services.AddTransient<PricingService>();

        return services;
    }
}
