using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using UtilityBot.Models;

namespace UtilityBot.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private Session _session;
        

        public TextMessageController(ITelegramBotClient telegramClient, Session s)
        {
            _telegramClient = telegramClient;
            _session= s;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            if (message.Text == "/start")
            {
                var buttons = new List<InlineKeyboardButton[]>();
                buttons.Add(new[]{
                        InlineKeyboardButton.WithCallbackData($" Посчитать символы" , $"simv"),
                        InlineKeyboardButton.WithCallbackData($" Сложение" , $"summ")
                        });

                await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>  Наш бот умеет считать количество букв в сообщении.</b> {Environment.NewLine}" +
                    $"{Environment.NewLine}<b>  А так же проводить сложение чисел написаных через пробел.</b>{Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
                return;
            }
            else
            {
                _session.UserTextMessage = message.Text;
                return;
            }
        }
    }
}
