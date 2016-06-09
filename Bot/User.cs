using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot {
    public class User : IDisposable
    {

        public long ChatId { get; set; }
        public MsgOffset Offset { get; set; }
        public IBot bot;

        public User()
        {
            this.Offset = new MsgOffset();
        }

        public void Dispose() {
            bot.CloseUser(ChatId);
        }
    }
}
