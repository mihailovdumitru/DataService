using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DBObjects
{
    public class TestResults
    {
        public int StudentID { get; set; }
        public int TestID { get; set; }
        public float Mark { get; set; }
        public float Points { get; set; }
        public string AnswersResult { get; set; }
        public DateTime TestResultDate { get; set; }
        public int NrOfCorrectAnswers { get; set; }
        public int NrOfWrongAnswers { get; set; }
        public int NrOfUnfilledAnswers { get; set; }
    }
}
