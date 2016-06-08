using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot;

namespace FilmAdvisor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var c = new Controller();
            c.StartControlling();
            //var genre = new Question("Введите жанр", QuestionType.Genre, "");
            //var year = new Question("Введите год", QuestionType.Year, "");
            //var country = new Question("Введите страну", QuestionType.Country, "");
            //var rate = new Question("Введите рейтинг", QuestionType.Rate, "");

            //var questions = new[] { genre, year, country, rate };

            //var bot = new Bot();

            //bot.Run();

            //foreach (var q in questions)
            //    bot.Ask(q);
            

            //bot.SendMessage("Genre: " + genre.Answer);
            //bot.SendMessage("Year: " + year.Answer);
            //bot.SendMessage("Country: " + country.Answer);
            //bot.SendMessage("Rate: " + rate.Answer);
        }
    }
}
