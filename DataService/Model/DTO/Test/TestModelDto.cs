using System.Collections.Generic;

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