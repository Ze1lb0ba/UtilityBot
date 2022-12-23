using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions;
using Telegram.Bot;
using UtilityBot;
using UtilityBot.Configuration;
using UtilityBot.Controllers;
using UtilityBot.Models;
using UtilityBot.Services;
using UtilityBot.Function;

namespace UtilityBot
{
    public class Program
    {
        public static async Task Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            // Объект, отвечающий за постоянный жизненный цикл приложения
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services)) // Задаем конфигурацию
                .UseConsoleLifetime() // Позволяет поддерживать приложение активным в консоли
                .Build(); // Собираем

            Console.WriteLine("Сервис запущен");
            // Запускаем сервис
            await host.RunAsync();
            Console.WriteLine("Сервис остановлен");

        }

        static void ConfigureServices(IServiceCollection services)
        {
            AppSettings app = new AppSettings();
            // Регистрируем объект TelegramBotClient c токеном подключения
            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(app.BotToken));
            // Регистрируем постоянно активный сервис бота
            services.AddHostedService<Bot>();
            services.AddTransient<TextMessageController>();
            services.AddTransient<DefaultMessageController>();
            services.AddTransient<InlineKeyBoardController>();
            services.AddSingleton<SelectOfUser>();
            services.AddSingleton<Session>();
            services.AddSingleton<IStorage, MemoryStorage>();
            services.AddTransient<CountOfLetter>();
            services.AddTransient<SummInt>();
        }
    }
}
