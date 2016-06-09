using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot;

namespace FilmAdvisor {
    public class Program {
        public static async void Start(Controller c, User user) {
            await c.ProcessUserAsync(user);
        }

        public static void Main(string[] args) {
            var bot = new Bot();
            var c = new Controller(new Parser(), new Requester(), bot);
            
            while (true) {
                var user = new User();
                bot.Run(user);
                user.ChatId = bot.GetUser();
                user.bot = bot;
                Start(c, user);
            }
        }
    }
}