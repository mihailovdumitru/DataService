using System.Collections.Generic;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Model.DTO.Test;
using Persistance.Facade.Interfaces;
using Persistance.Interfaces;

namespace Test.Controllers
{
    [Produces("application/json")]
    [Route("api/Test")]
    public class TestController : Controller
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly ITestRepository testRepo;
        private readonly ITestFacade testFacade;

        public TestController(ITestRepository testRepo, ITestFacade testFacade)
        {
            this.testRepo = testRepo;
            this.testFacade = testFacade;
        }

        // POST: api/Test
        [HttpPost]
        public ActionResult Post([FromBody]TestModelDto test)
        {
            test.LectureID = 1;
            test.TeacherID = 21;
            int testId = testFacade.AddTestObject(test);

            _log.Info("Insert a new test: " + test.Naming);

            return Ok("Succes");
        }
    }
}