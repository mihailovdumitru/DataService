using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTO.Test;
using Model.Teacher;
using Persistance.Facade.Interfaces;
using Persistance.Interfaces;

namespace Test.Controllers
{
    [Produces("application/json")]
    [Route("api/Test")]
    public class TestController : Controller
    {
        private readonly ITestRepository testRepo;
        private readonly ITestFacade testFacade;

        public TestController(ITestRepository testRepo, ITestFacade testFacade)
        {
            this.testRepo = testRepo;
            this.testFacade = testFacade;
        }

        // GET: api/Test
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Test/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Test
        [HttpPost]
        //[EnableCors("UrlPolicy")]
        public ActionResult Post([FromBody]TestModelDto test)
        {
            test.LectureID = 1;
            test.TeacherID = 1;
            int testId = testFacade.AddTestObject(test);
            


            return Ok("Cu succes");
        }
        
        // PUT: api/Test/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
