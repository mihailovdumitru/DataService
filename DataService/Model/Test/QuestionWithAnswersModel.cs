using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Test
{
    public class QuestionWithAnswers
    {
        public QuestionModel Question { get; set; }
        public List<AnswerModel> Answers { get; set; }
    }
}
