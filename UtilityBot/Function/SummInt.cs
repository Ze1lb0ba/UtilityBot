using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using UtilityBot.Models;

namespace UtilityBot.Function
{
    public class SummInt
    {
        private Session _session;
        private readonly ITelegramBotClient _botClient;

        public SummInt(Session session, ITelegramBotClient botClient)   
        {
            _session = session;
            _botClient = botClient;
        }

        public async Task Summ()
        {
            if (_session.UserTextMessage != null)
            {
                string basic = _session.UserTextMessage;
                string[] strArr = basic.Split(" ");
                bool[] bools = new bool[strArr.Length];
                int[] ints = new int[strArr.Length];
                int i = 0;
                bool check = true;
                foreach (string str in strArr)
                {
                    bools[i] = Int32.TryParse(str, out int j);
                    ints[i] = j;
                    i++;
                }
                for (int j = 0; j < bools.Length; j++)
                {
                    if (bools[j]) { }
                    else
                    {
                        check = false;
                    }
                }

                if (check == true)
                {
                    int a = ints[0];
                    for (int j = 1; j < ints.Length; j++)
                    {
                        a += ints[j];
                    }

                    basic = "Сумма введенных чисел равна: " + a;
                }
                else basic = "В введенном вами числовом ряду встречаются буквы";

                _botClient.SendTextMessageAsync(_session.UserID, basic);
                _session.UserTextMessage = null;
                Summ();
            }
            else 
            {
                Thread.Sleep(3000);
                Summ();            
            }
        }
    }
}
