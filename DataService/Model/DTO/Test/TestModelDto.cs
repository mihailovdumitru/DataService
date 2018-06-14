using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO.Test
{
    public class TestModelDto
    {
        public int TestID { get; set; }
        public int LectureID { get; set; }
        public string Naming { get; set; }
        public List<QuestionWithAnswersDto> Questions { get; set; }
        public int TeacherID { get; set; }
    }
}
