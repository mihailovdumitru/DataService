using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DBObjects;
using Persistance.Interfaces;

namespace DataService.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Classes")]
    public class ClassesController : Controller
    {
        private readonly IClassRepository classRepo;

        public ClassesController(IClassRepository classRepo)
        {
            this.classRepo = classRepo;
        }

        // GET: api/Classes
        [HttpGet]
        public IEnumerable<StudyClass> Get()
        {
            return classRepo.GetClasses();
        }

        // GET: api/Classes/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Classes
        [HttpPost]
        public int Post([FromBody]StudyClass studyClass)
        {
            return classRepo.AddOrUpdateClass(studyClass);
        }
        
        // PUT: api/Classes/5
        [HttpPut("{id}")]
        public int Put(int id, [FromBody]StudyClass studyClass)
        {
            return classRepo.AddOrUpdateClass(studyClass, null, id);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
