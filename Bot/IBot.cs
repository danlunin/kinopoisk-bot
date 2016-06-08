using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public interface IBot
    {
        long GetUser();

        void CloseUser(long chatId);

        void Run(MsgOffset offset);


        void Ask(Question q, long chatId, MsgOffset offset);

    }
}