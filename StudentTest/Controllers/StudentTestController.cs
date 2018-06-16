using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Model.DBObjects;
using Model.DTO.Test;
using Persistance.Facade.Interfaces;
using Persistance.Interfaces;

namespace StudentTest.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    //[Route("api/[controller]")]
    public class StudentTestController : Controller
    {
        private readonly ITestFacade testFacade;
        private readonly ITestParametersRepository testParamRepo;
        private readonly ITestResultsRepository testResultsRepo;

        public StudentTestController(ITestFacade testFacade, ITestParametersRepository testParamRepo, ITestResultsRepository testResultsRepo)
        {
            this.testFacade = testFacade;
            this.testParamRepo = testParamRepo;
            this.testResultsRepo = testResultsRepo;
        }

        // GET: api/StudentTest
        [HttpGet("{id}")]
        public TestModelDto GetTest(int id)
        {
            return testFacade.GetTestObject(id);
        }

        [HttpGet]
        public List<TestParameters> GetTestsParameters()
        {
            return testParamRepo.GetTestParameters();
        }

        // POST: api/StudentTest
        [HttpPost]
        public bool TestResults([FromBody]TestResults testResults)
        {
            return testResultsRepo.AddTestResults(testResults);
        }

        [HttpGet]
        public List<TestResults> TestResults()
        {
            return testResultsRepo.GetTestsResults();
        }
    }
}