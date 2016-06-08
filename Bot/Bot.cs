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

namespace TelegramBot {
    public class MsgOffset {
        public int Offset { get; set; } = 0;
    }

    public class Bot :IBot {
        public TelegramBotClient bot { get; set; }
        //public int Offset { get; set; } = 0;
        //public long ChatId { get; set; }
        public Queue<long> Users { get; set; } = new Queue<long>();
        public List<long> ProcessingUsers { get; set; } = new List<long>();

        public Bot() {
            bot = new TelegramBotClient(Config.Token);
        }

        public long GetUser() {
            return Users.Dequeue();
        }

        public void CloseUser(long chatId) {
            SendMessage("Enjoy your time!", chatId);
            ProcessingUsers.Remove(chatId);
        }

        public void Run(MsgOffset offset) {
            StartAsync(offset).Wait();
        }

        private async Task StartAsync(MsgOffset msgOffset, string startMessage = "/start") {
            while (true) {
                var updates = await bot.GetUpdatesAsync(msgOffset.Offset);
                foreach (var update in updates) {
                    msgOffset.Offset = update.Id + 1;
                    if (update.Message.Text == startMessage && !ProcessingUsers.Contains(update.Message.Chat.Id)) {
                        Users.Enqueue(update.Message.Chat.Id);
                        ProcessingUsers.Add(update.Message.Chat.Id);
                        return;
                    }
                }
            }
        }


        public void Ask(Question q, long chatId, MsgOffset offset) {
            _Ask(q, chatId).Wait();
            GetAnswer(q, chatId, offset).Wait();
        }

        private async Task _Ask(Question q, long chatId) {
            await bot.SendChatActionAsync(chatId, ChatAction.Typing);
            await Task.Delay(1000);
            await bot.SendTextMessageAsync(chatId, q.QuestionText);
        }

        private async Task GetAnswer(Question question, long chatId, MsgOffset msgOffset) {
            while (true) {
                var updates = await bot.GetUpdatesAsync(msgOffset.Offset);
                foreach (var update in updates) {
                    msgOffset.Offset = update.Id + 1;
                    if (update.Message.Chat.Id == chatId) {
                        question.Answer = update.Message.Text;
                        return;
                    }
                }
            }
        }

        public void SendMessage(string message, long chatId) {
            _SendMessage(message, chatId).Wait();
        }

        private async Task _SendMessage(string message, long chatId) {
            await bot.SendTextMessageAsync(chatId, message);
        }
    }
}