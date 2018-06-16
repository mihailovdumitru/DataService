using Model.DBObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Persistance.Interfaces
{
    public interface IQuestionRepository
    {
        int AddQuestion(Question test, SqlConnection conn = null);
        List<Question> GetQuestionsByTestID(int testID, SqlConnection conn = null);
        int UpdateQuestion(Question question, SqlConnection conn = null);
    }
}
