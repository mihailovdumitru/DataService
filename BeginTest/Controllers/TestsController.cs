using System.Collections.Generic;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Model.DBObjects;
using Persistance.Interfaces;

namespace BeginTest.Controllers
{
    [Produces("application/json")]
    [Route("api/Tests")]
    public class TestsController : Controller
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
            _log.Info("Get the tests.");

            return testRepo.GetTests();
        }

        // POST: api/Tests
        [HttpPost]
        public bool Post([FromBody]TestParameters testParams)
        {
            _log.Info("Add test parameters.");

            return testParamRepo.AddTestParameters(testParams);
        }
    }
}