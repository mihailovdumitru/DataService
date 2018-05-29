using Model.DBObjects;
using Model.Teacher;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Persistance.Interfaces
{
    public interface ITestRepository
    {
        int AddTeacher(Teacher teacher);
        int AddTest(Test test, SqlConnection conn = null);
        int AddQuestion(Question test, SqlConnection conn = null);
        int AddAnswer(Answer answer, SqlConnection conn = null);
        void AddQuestionAnswer(int questionID, int answerID, bool correct, SqlConnection conn = null);
    }
}
