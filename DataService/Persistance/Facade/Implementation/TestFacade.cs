using AutoMapper;
using Model.DBObjects;
using Model.DTO.Test;
using Persistance.Facade.Interfaces;
using Persistance.Interfaces;
using Persistance.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Persistance.Facade.Implementation
{
    public class TestFacade: SqlBase, ITestFacade
    {
        private readonly IMapper mapper;
        private readonly ITestRepository testRepo;
        private readonly IQuestionRepository questionRepo;
        private readonly IAnswerRespository answerRepo;
        private readonly IQuestionAnswerRespository questionAnswerRepo;

        public TestFacade(IMapper mapper,ITestRepository testRepo,IQuestionRepository questionRepo,
                          IAnswerRespository answerRepo,IQuestionAnswerRespository questionAnswerRepo)
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
                foreach(var question in test.Questions)
                {
                    questionObj = mapper.Map<QuestionModelDto, Question>(question.Question);
                    questionObj.TestID = testId;
                    questionID = questionRepo.AddQuestion(questionObj, conn);
                    foreach(var answer in question.Answers)
                    {
                        answerObj = mapper.Map<AnswerModelDto, Answer>(answer);
                        answerID = answerRepo.AddAnswer(answerObj, conn);
                        questionAnswerRepo.AddQuestionAnswer(questionID, answerID, answer.Correct, conn);
                    }
                }
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            
            return testId;
        }
    }
}
