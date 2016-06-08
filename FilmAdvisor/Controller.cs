using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using TelegramBot;
using System.Text.RegularExpressions;


namespace FilmAdvisor
{
    public class Controller : IController
    {
        private IParser parser;
        private IRequester requester;
        //private IBot bot;
        public Controller(IParser parser, IRequester requester, IBot bot)
        {
            this.parser = parser;
            this.requester = requester;
            //this.bot = bot;
        }
        public Question[] GetQuestions() {
            var genre = new Question("Введите жанр", QuestionType.Genre, "");
            var year = new Question("Введите год", QuestionType.Year, @"\d\d\d\d");
            var country = new Question("Введите страну", QuestionType.Country, "");
            //var rate = new Question("Введите рейтинг", QuestionType.Rate, "");

            return new[] { genre, year, country };
        }

        public IEnumerable<Question> MakeQuestions(Dictionary<String, QuestionType> parameters)
        {
            foreach (var key in parameters.Keys)
                yield return new Question(key, parameters[key], "");
        }

        public bool CheckAnswer(Question question)
        {
            return Regex.IsMatch(question.Answer, question.Pattern);

        }

        public IEnumerable<KeyValuePair<string, object>> GetDictionaryForRequest(
            Question[] questions,
            IBot bot, long id, MsgOffset msg) {
            foreach (var q in questions) {
                bot.Ask(q, id, msg);
                if (CheckAnswer(q)) {
                    Console.WriteLine(q.QuestionType.ToString() + q.Answer);
                    yield return new KeyValuePair<string, object>(q.QuestionType.ToString(), q.Answer);
                }
            }
        }

        public List<IFilm> EarnFilms(IEnumerable<KeyValuePair<string, object>> requestDict, 
            int amountOfFilms)
        {
            var rawResponse = requester.Search(new Parameters(requestDict));
            var films = parser.GetFilms(rawResponse);
            return films.Take(amountOfFilms).ToList();
        }

        public async Task ProcessUserAsync(IBot bot, long id, MsgOffset msgOffset) {
            var questions = GetQuestions();
            var request = GetDictionaryForRequest(questions, bot, id, msgOffset);
            var films = EarnFilms(request, 10);
            foreach (var film in films) {
                Console.WriteLine(film.Name);
                Console.WriteLine(film);
            }
            bot.CloseUser(id);
        }
    }
}
