using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TelegramBot;

namespace FilmAdvisor
{
    public interface IBot
    {
        void Run();
        string Ask(Question question);
        void SendMessage(string text);
    }
}
