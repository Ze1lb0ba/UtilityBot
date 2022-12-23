using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using UtilityBot.Models;

namespace UtilityBot.Function
{
     public class CountOfLetter
    {
        private readonly ITelegramBotClient _telegrammBot;
        private Session _session;

        public CountOfLetter(ITelegramBotClient telegrammBot,Session session)
        {
            _telegrammBot = telegrammBot;
            _session = session;
        }

        public async Task NumberOfLetter()
        {
            if (_session.UserTextMessage != null)
            {
                _telegrammBot.SendTextMessageAsync(_session.UserID, $"В отправленном вами сообщении " + _session.UserTextMessage.Length + " символов");
                _session.UserTextMessage = null;
                NumberOfLetter();
            }
            else
            {
                Thread.Sleep(3000);
                NumberOfLetter();
            }
        }
    }
}
