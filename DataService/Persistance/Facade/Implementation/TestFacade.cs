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
        public TestFacade(IMapper mapper,ITestRepository testRepo)
        {
            this.mapper = mapper;
            this.testRepo = testRepo;
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
                    questionID = testRepo.AddQuestion(questionObj, conn);
                    foreach(var answer in question.Answers)
                    {
                        answerObj = mapper.Map<AnswerModelDto, Answer>(answer);
                        answerID = testRepo.AddAnswer(answerObj, conn);
                        testRepo.AddQuestionAnswer(questionID, answerID, answer.Correct, conn);
                    }
                }
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            
            return testId;
        }


        public Test TestModelDtoToTest(TestModelDto test)
        {
            return new Test()
            {
                Naming = test.Naming,
                TeacherID = test.TeacherID,
                LectureID = test.LectureID
            };
        }
    }
}
