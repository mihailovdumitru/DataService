using Model.DBObjects;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Persistance.Interfaces
{
    public interface IAnswerRespository
    {
        int AddAnswer(Answer answer, SqlConnection conn = null);
        List<Answer> GetAnswers(SqlConnection conn = null);
        int UpdateAnswer(Answer answer, SqlConnection conn = null);
    }
}