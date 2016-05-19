using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Bot
{
    class Program
    {
        static void Main(string[] args)
        {
            var Bot = new Api(Config.Token);
            var me = Bot.GetMe().Result;
            System.Console.WriteLine("Hello my name is " + me.FirstName);
        }
    }
}
