using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using UtilityBot.Services;

namespace UtilityBot.Controllers
{
    public class InlineKeyBoardController
    {
        private readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _telegramClient;
        private SelectOfUser _selectOfUser;

        public InlineKeyBoardController(ITelegramBotClient telegramBotClient, IStorage memoryStorage, SelectOfUser selectOfUser )
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
            _selectOfUser= selectOfUser;
        }

        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery?.Data == null)
                return;

            // Обновление пользовательской сессии новыми данными
            _memoryStorage.GetSession(callbackQuery.From.Id).ModeSelected = callbackQuery.Data;

            // Генерим информационное сообщение
            string userSelect = callbackQuery.Data switch
            {
                "simv" => "Посчитать символы",
                "summ" => "Посчитать сумму чисел",
                _ => String.Empty
            };

            // Отправляем в ответ уведомление о выборе
            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
                $"<b>Вы выбрали - {userSelect}.{Environment.NewLine}</b>" +
                $"{Environment.NewLine}Можно поменять в главном меню.", cancellationToken: ct, parseMode: ParseMode.Html);
            if (userSelect == "Посчитать сумму чисел")
            {
                await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id, $"{Environment.NewLine}Для этого необходимо ввести любое количество чисел, через пробел.");
            }
            else await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id, $"{Environment.NewLine}Для этого просто пришлите сюда текст длинну которого необходимо посчитать.");
            _selectOfUser.Selecter(userSelect);
        } 
    }
}
