using System.Collections.Generic;

namespace Model.DTO.Test
{
    public class QuestionWithAnswersDto
    {
        public QuestionModelDto Question { get; set; }
        public List<AnswerModelDto> Answers { get; set; }
    }
}