using AutoMapper;
using Model.DBObjects;
using Model.DTO.Test;
using Persistance.Facade.Interfaces;
using Persistance.Interfaces;
using Persistance.Utilities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Persistance.Facade.Implementation
{
    public class TestFacade : SqlBase, ITestFacade
    {
        private readonly IMapper mapper;
        private readonly ITestRepository testRepo;
        private readonly IQuestionRepository questionRepo;
        private readonly IAnswerRespository answerRepo;
        private readonly IQuestionAnswerRespository questionAnswerRepo;

        public TestFacade(IMapper mapper, ITestRepository testRepo, IQuestionRepository questionRepo,
                          IAnswerRespository answerRepo, IQuestionAnswerRespository questionAnswerRepo)
        {
            this.mapper = mapper;
            this.testRepo = testRepo;
            this.questionRepo = questionRepo;
            this.answerRepo = answerRepo;
            this.questionAnswerRepo = questionAnswerRepo;
        }

        public int AddTestObject(TestModelDto test)
        {
            int testId = -1;
            int questionID;
            int answerID;
            Test testObj = null;
            Question questionObj = null;
            Answer answerObj = null;

            using (var conn = new SqlConnection(base.GetConnectionString()))
            {
                conn.Open();
                testObj = mapper.Map<TestModelDto, Test>(test);
                testId = testRepo.AddTest(testObj, conn);

                if (test.Questions != null)
                {
                    foreach (var question in test.Questions)
                    {
                        questionObj = mapper.Map<QuestionModelDto, Question>(question.Question);
                        questionObj.TestID = testId;
                        questionID = questionRepo.AddQuestion(questionObj, conn);
                        if (question.Answers != null)
                        {
                            foreach (var answer in question.Answers)
                            {
                                answerObj = mapper.Map<AnswerModelDto, Answer>(answer);
                                answerID = answerRepo.AddAnswer(answerObj, conn);
                                questionAnswerRepo.AddQuestionAnswer(questionID, answerID, answer.Correct, conn);
                            }
                        }
                    }
                }

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return testId;
        }

        public TestModelDto GetTestObject(int id)
        {
            TestModelDto testObj = null;
            QuestionModelDto questionObj = null;
            AnswerModelDto answerObj = null;
            QuestionWithAnswersDto questionWithAnswersObj = null;
            List<AnswerModelDto> answersList = null;

            using (var conn = new SqlConnection(base.GetConnectionString()))
            {
                conn.Open();
                var test = testRepo.GetTests(conn).First(obj => obj.TestID == id);
                testObj = mapper.Map<Test, TestModelDto>(test);

                var questions = questionRepo.GetQuestionsByTestID(testObj.TestID, conn);
                var questionAnswers = questionAnswerRepo.GetQuestionAnswers(conn);
                var answers = answerRepo.GetAnswers(conn);

                testObj.Questions = new List<QuestionWithAnswersDto>();

                foreach (var question in questions)
                {
                    questionObj = mapper.Map<Question, QuestionModelDto>(question);

                    var questAnsw = questionAnswers.Where(q => q.QuestionID == question.QuestionID);
                    answersList = new List<AnswerModelDto>();
                    foreach (var qAnsw in questAnsw)
                    {
                        var answer = answers.First(x => x.AnswerID == qAnsw.AnswerID);
                        answerObj = mapper.Map<Answer, AnswerModelDto>(answer);
                        answerObj.Correct = qAnsw.Correct;
                        answersList.Add(answerObj);
                    }

                    questionWithAnswersObj = new QuestionWithAnswersDto()
                    {
                        Question = questionObj,
                        Answers = answersList
                    };

                    testObj.Questions.Add(questionWithAnswersObj);
                }

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return testObj;
        }
    }
}