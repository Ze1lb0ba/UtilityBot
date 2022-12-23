using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using UtilityBot.Function;
using UtilityBot.Models;

namespace UtilityBot.Controllers
{
    public class SelectOfUser
    {
        private readonly ITelegramBotClient _botClient;
        private CountOfLetter _letter;
        private Session _session;
        private SummInt _summ;
        
        
        public SelectOfUser(ITelegramBotClient botClient, CountOfLetter letter, Session session, SummInt summ)
        {
            _botClient = botClient;
            _letter = letter;
            _session= session;
            _summ = summ;
        }

        public void Selecter(string userSellect)
        {
            if(userSellect == "Посчитать символы")
            {
                _botClient.SendTextMessageAsync(_session.UserID, $"В вашем сообщении {_letter.NumberOfLetter()} символов");
            }
            else
            {
                _summ.Summ();
            }

        }
    }
}
