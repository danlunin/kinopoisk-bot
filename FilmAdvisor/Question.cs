using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmAdvisor
{
    public enum QuestionType
    {
        Year,
        Genre,
        Name,
        BorderRate
    }

    public class Question
    {
        public string Text { get; }
        public QuestionType QuestionType { get; }
        public string Pattern { get; }

        public Question(QuestionType type, string txt) {
            Text = txt;
            QuestionType = type;
        }
    }
}
