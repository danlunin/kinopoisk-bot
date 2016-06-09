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
        public Controller(IParser parser, IRequester requester, IBot bot)
        {
            this.parser = parser;
            this.requester = requester;
        }
        public Question[] GetQuestions() {
            var genre = new Question("Введите жанр", QuestionType.Genre, "");
            var year = new Question("Введите год", QuestionType.Year, @"\d\d\d\d");
            var country = new Question("Введите страну", QuestionType.Country, "");

            return new[] { genre, year, country };
        }


        public bool CheckAnswer(Question question)
        {
            return Regex.IsMatch(question.Answer, question.Pattern);

        }

        public IEnumerable<KeyValuePair<string, object>> GetDictionaryForRequest(
            Question[] questions, User user) {
            foreach (var q in questions) {
                user.bot.Ask(q, user.ChatId, user.Offset);
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

        public async Task ProcessUserAsync(User user) {
            using (user) {
                var questions = GetQuestions();
                var request = GetDictionaryForRequest(questions, user);
                var films = EarnFilms(request, 10);
                foreach (var film in films) {
                    Console.WriteLine(film.Name);
                    Console.WriteLine(film);
                }
            }
            
        }
    }
}
