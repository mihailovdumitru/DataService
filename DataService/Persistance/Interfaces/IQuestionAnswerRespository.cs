using Model.DBObjects;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Persistance.Interfaces
{
    public interface IQuestionAnswerRespository
    {
        void AddOrUpdateQuestionAnswer(int questionID, int answerID, bool correct, SqlConnection conn = null);
        List<QuestionWithAnswers> GetQuestionAnswers(SqlConnection conn = null);
    }
}