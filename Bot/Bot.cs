using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Helpers;
using System.Net.Http;
using Telegram.Bot.Types.Enums;

namespace TelegramBot
{
    public class Bot
    {
        public TelegramBotClient bot { get; set; }
        public int Offset { get; set; } = 0;
        public long ChatId { get; set; }

        public Bot()
        {
            bot = new TelegramBotClient(Config.Token);
        }

        public void Run()
        {
            StartAsync().Wait();
        }

        private async Task StartAsync(string startMessage = "/start")
        {
            while (true)
            {
                var updates = bot.GetUpdatesAsync(Offset).Result;
                // (var update in updates)
                //{
                if (updates.Length > 0)
                {
                    var update = updates[updates.Length - 1];
                    Offset = update.Id + 1;
                    if (update.Message.Text == startMessage)
                    {
                        ChatId = update.Message.Chat.Id;
                        return;
                    }
                }
                //}
            }

            //while (true)
            //{
            //    var isStart = await WaitMessageAsync();
            //    if (isStart)
            //        break;
            //}
            //foreach (var q in questions)
            //{
            //    await Bot.SendChatAction(ChatId, ChatAction.Typing);
            //    await Task.Delay(1000);
            //    await Bot.SendTextMessage(ChatId, q.QuestionText);

            //    GetAnswer(q).Wait();
            //}
            //await Task.Delay(1000);
        }

        public void Ask(Question q)
        {
            _Ask(q).Wait();
        }

        private async Task _Ask(Question q)
        {
            
            await bot.SendChatActionAsync(ChatId, ChatAction.Typing);
            await Task.Delay(1000);
            await bot.SendTextMessageAsync(ChatId, q.QuestionText);

            GetAnswer(q).Wait();
        }

        //public async Task<bool> WaitMessageAsync(string startMessage = "/start")
        //{
        //    var updates = await Bot.GetUpdates(Offset);
        //    foreach (var update in updates)
        //    {
        //        Offset = update.Id + 1;
        //        if (update.Message.Text == startMessage)
        //        {
        //            ChatId = update.Message.Chat.Id;
        //            return true;
        //        }
        //    }
        //    return false;
        //}
        
        private async Task GetAnswer(Question question)
        {
            while (true)
            {
                var updates = await bot.GetUpdatesAsync(Offset);
                foreach (var update in updates)
                {
                    question.Answer = update.Message.Text;
                    Offset = update.Id + 1;
                    return;
                }
            }
        }
        
        public void SendMessage(string message)
        {
            _SendMessage(message).Wait();
        }

        private async Task _SendMessage(string message)
        {
            await bot.SendTextMessageAsync(ChatId, message);
        }
    }
}
