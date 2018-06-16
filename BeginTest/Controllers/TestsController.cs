using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Model.DBObjects;
using Persistance.Interfaces;

namespace BeginTest.Controllers
{
    [Produces("application/json")]
    [Route("api/Tests")]
    public class TestsController : Controller
    {
        private readonly ITestRepository testRepo;
        private readonly ITestParametersRepository testParamRepo;

        public TestsController(ITestRepository testRepo, ITestParametersRepository testParamRepo)
        {
            this.testRepo = testRepo;
            this.testParamRepo = testParamRepo;
        }

        // GET: api/Tests
        [HttpGet]
        public IEnumerable<Test> Get()
        {
            return testRepo.GetTests();
        }

        // POST: api/Tests
        [HttpPost]
        public bool Post([FromBody]TestParameters testParams)
        {
            return testParamRepo.AddTestParameters(testParams);
        }
    }
}