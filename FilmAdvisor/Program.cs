using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot;

namespace FilmAdvisor {
    public class Program {
        public static async void Start(Controller c, Bot bot, long id, MsgOffset msgOffset) {
            await c.ProcessUserAsync(bot, id, msgOffset);
        }

        public static void Main(string[] args) {
            var bot = new Bot();
            var c = new Controller(new Parser(), new Requester(), bot);
            
            while (true) {
                var msgOffset = new MsgOffset();
                bot.Run(msgOffset);
                Start(c, bot, bot.GetUser(), msgOffset);
            }
        }
    }
}