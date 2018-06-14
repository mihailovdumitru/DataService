using Model.DBObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Persistance.Interfaces
{
    public interface IQuestionAnswerRespository
    {
        void AddQuestionAnswer(int questionID, int answerID, bool correct, SqlConnection conn = null);
        List<QuestionWithAnswers> GetQuestionAnswers(SqlConnection conn = null);
    }
}
