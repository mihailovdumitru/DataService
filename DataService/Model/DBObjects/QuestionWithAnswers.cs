namespace Model.DBObjects
{
    public class QuestionWithAnswers
    {
        public int QuestionID { get; set; }
        public int AnswerID { get; set; }
        public bool Correct { get; set; }
    }
}