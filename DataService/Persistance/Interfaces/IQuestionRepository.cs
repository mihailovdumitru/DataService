using Model.DBObjects;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Persistance.Interfaces
{
    public interface IQuestionRepository
    {
        int AddQuestion(Question test, SqlConnection conn = null);
        List<Question> GetQuestionsByTestID(int testID, SqlConnection conn = null);
        int UpdateQuestion(Question question, SqlConnection conn = null);
    }
}