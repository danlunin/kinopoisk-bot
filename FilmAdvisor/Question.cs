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
        Name
    }

    public class Question
    {
        string text;
        QuestionType questionType;
        string pattern;
    }
}
