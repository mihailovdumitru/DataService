namespace Model.DBObjects
{
    public class Question
    {
        public int QuestionID { get; set; }
        public string Content { get; set; }
        public int Points { get; set; }
        public int TestID { get; set; }
    }
}