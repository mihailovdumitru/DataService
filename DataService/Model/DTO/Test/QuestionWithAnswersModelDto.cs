using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO.Test
{
    public class QuestionWithAnswersDto
    {
        public QuestionModelDto Question { get; set; }
        public List<AnswerModelDto> Answers { get; set; }
    }
}
