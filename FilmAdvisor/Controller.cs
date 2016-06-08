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
    class Controller : IController
    {
        public IEnumerable<Question> MakeQuestions(Dictionary<String, QuestionType> parameters)
        {
            foreach (var key in parameters.Keys)
                yield return new Question(key, parameters[key], "");
        }

        public bool CheckAnswer(Question question)
        {
            return true;
            //return Regex.IsMatch(question.Answer, question.Pattern);
            //try
            //{
            //    if (question.QuestionType.Equals(QuestionType.Year))
            //        Int32.Parse(answer);
            //    else if (question.QuestionType.Equals(QuestionType.BorderRate))
            //        Double.Parse(answer);
            //    else return true;
            //    return true;
            //}
            //catch (System.FormatException)
            //{
            //    return false;
            //}

        }

        public IEnumerable<KeyValuePair<string, object>> GetDictionaryForRequest(
            Dictionary<String, QuestionType> questions,
            Bot bot)
        {
            foreach (var q in MakeQuestions(questions))
            {
                bot.Ask(q);
                if (CheckAnswer(q))
                    yield return new KeyValuePair<string, object>(q.QuestionType.ToString(), q.Answer);
            }
        }

        public IEnumerable<IFilm> EarnFilms(IEnumerable<KeyValuePair<string, object>> requestDict, int amountOfFilms)
        {
            var requester = new Requester();
            var response = requester.Search(new Parameters(requestDict));
            var parser = new Parser();
            return parser.GetFilms(response).Take(amountOfFilms);
        }

        public void StartControlling()
        {
            var questions = new Dictionary<String, QuestionType>();
            questions.Add("Note lowest border of the film's year", QuestionType.Year);
            questions.Add("Select genre", QuestionType.Genre);
            questions.Add("Choose film's country", QuestionType.Country);
            //questions.Add("Note lowest rate", QuestionType.BorderRate);
            var bot = new Bot();
            bot.Run();
            var requestDict = GetDictionaryForRequest(questions, bot);
            foreach (var film in EarnFilms(requestDict, 10))
            {
                bot.SendMessage(String.Format("Name: {0}, Country: {1}, Year: {2}, Rate: {3}, Producer{4}",
                    film.Name,
                    film.Country,
                    film.Year,
                    film.Rate,
                    film.Producer));  // -> В фильме переопределить ToString()
            }
            bot.SendMessage("Enjoy your time!");
        }

    }
}
