using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        // GET: api/Tests/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Tests
        [HttpPost]
        public bool Post([FromBody]TestParameters testParams)
        {
            return testParamRepo.AddTestParameters(testParams);
        }
        
        // PUT: api/Tests/5
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
