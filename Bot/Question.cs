using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public enum QuestionType
    {
        Year,
        Genre,
        Country,
        Rate,
        Other
    }

    public class Question
    {
        public Question(string questionText, QuestionType questionType, string pattern)
        {
            QuestionType = questionType;
            QuestionText = questionText;
            Pattern = pattern;
        }

        public string QuestionText;
        public QuestionType QuestionType;
        public string Pattern;

        public string Answer;
    }
}
