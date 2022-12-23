using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityBot.Models;

namespace UtilityBot.Services
{
    public class MemoryStorage : IStorage
    {
        /// <summary>
        /// Хранилище сессий
        /// </summary>
        private readonly ConcurrentDictionary<long, Session> _sessions;
        private Session _userID;

        public MemoryStorage(Session userID)
        {
            _sessions = new ConcurrentDictionary<long, Session>();
            _userID = userID;
        }

        public Session GetSession(long chatId)
        {
            // Возвращаем сессию по ключу, если она существует
            if (_sessions.ContainsKey(chatId))
            {
                _userID.UserID = chatId;
                return _sessions[chatId];
            }

                // Создаем и возвращаем новую, если такой не было
            var newSession = new Session() { ModeSelected = "simv" };
            _sessions.TryAdd(chatId, newSession);
            _userID.UserID = chatId;
            return newSession;
        }
    }
}
