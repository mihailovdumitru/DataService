using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Test
{
    public class QuestionWithAnswers
    {
        public Question Question { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
